using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace DungeonsAndDungeons.Generation
{
    public class Mapbuilder
    {
        public int[,] map;

        /// <summary>
        /// Built rooms stored here
        /// </summary>
        public List<Room> Rooms { get; private set; }

        /// <summary>
        /// Built corridors stored here
        /// </summary>
        private List<Point> BuiltCorridors { get; set; }

        /// <summary>
        /// Corridor to be built stored here
        /// </summary>
        private List<Point> PotentialCorridor { get; set; }

        /// <summary>
        /// Room to be built stored here
        /// </summary>
        private Room CurrentRoom { get; set; }
        public StartRoom StartRoom { get; private set; }
        public Size MinimumRoomSize { get; set; }
        public Size MaximumRoomSize { get; set; }
        public int MaxRooms { get; set; }
        public int RoomDistance { get; set; }
        public int CorridorDistance { get; set; }
        public int MinimumCorridorLength { get; set; }
        public int MaximumCorridorLength { get; set; }
        public int MaximumCorridorTurns { get; set; }
        public int CorridorSpace { get; set; }
        public int BuildProb { get; set; }
        public Size MapSize { get; set; }
        public int BreakOut { get; set; }

        /// <summary>
        /// describes the outcome of the corridor building operation
        /// </summary>
        enum CorridorItemHit
        {
            INVALID, //INVALID point generated
            SELF,//corridor hit SELF
            EXISTING_CORRIDOR,//hit a built corridor
            ORIGIN_ROOM,//corridor hit origin room 
            EXISTING_ROOM,//corridor hit existing room
            COMPLETED,//corridor built without problem    
            TOO_CLOSE,
            OK //point OK
        }

        Point[] Directions = new Point[]
        {
            new Point(0, -1), //n
            new Point(0, 1), //s
            new Point(1, 0),//w
            new Point(-1, 0)//e
        };

        private const int FILLED_CELL = 1;
        private const int EMPTY_CELL = 0;
        readonly Random rnd = new Random();

        public Mapbuilder(int x, int y, int maxTurns, Size roomMin, Size roomMax, Range<int> corridorRange, int maxRooms, int roomDistance, int corridorDistance, int corridorSpace)
        {
            MapSize = new Size(x, y);
            map = new int[MapSize.Width, MapSize.Height];
            MaximumCorridorTurns = maxTurns;
            MinimumRoomSize = roomMin;
            MaximumRoomSize = roomMax;
            MinimumCorridorLength = corridorRange.Minimum;
            MaximumCorridorLength = corridorRange.Maximum;
            MaxRooms = maxRooms;
            //MapSize = new Size(150, 150);

            RoomDistance = roomDistance;
            CorridorDistance = corridorDistance;
            CorridorSpace = corridorSpace;

            BuildProb = 50;
            BreakOut = 250;
        }

        /// <summary>
        /// Initialise everything
        /// </summary>
        private void Clear()
        {
            PotentialCorridor = new List<Point>();
            Rooms = new List<Room>();
            BuiltCorridors = new List<Point>();

            map = new int[MapSize.Width, MapSize.Height];
            for (int x = 0; x < MapSize.Width; x++)
                for (int y = 0; y < MapSize.Width; y++)
                    map[x, y] = FILLED_CELL;
        }

        /// <summary>
        /// Randomly choose a room and attempt to build a corridor terminated by
        /// a room off it, and repeat until MaxRooms has been reached. The map
        /// is started of by placing a room in approximately the centre of the map
        /// using the method PlaceStartRoom()
        /// </summary>
        /// <returns>Bool indicating if the map was built, i.e. the property BreakOut was not
        /// exceed</returns>
        public bool BuildStartRoom()
        {
            int loopctr = 0;

            CorridorItemHit CorBuildOutcome;
            Point Location = new Point();
            Point Direction = new Point();

            Clear();

            PlaceStartRoom();

            //attempt to build the required number of rooms
            while (Rooms.Count() < MaxRooms)
            {

                if (loopctr++ > BreakOut)//bail out if this value is exceeded
                    return false;

                if (GetCorridorStart(out Location, out Direction))
                {

                    CorBuildOutcome = MakeCorridorStraight(ref Location, ref Direction, rnd.Next(1, MaximumCorridorTurns)
                        , rnd.Next(0, 100) > 50 ? true : false);

                    switch (CorBuildOutcome)
                    {
                        case CorridorItemHit.EXISTING_ROOM:
                        case CorridorItemHit.EXISTING_CORRIDOR:
                        case CorridorItemHit.SELF:
                            BuildCorridor();
                            break;

                        case CorridorItemHit.COMPLETED:
                            if (Room_AttemptBuildOnCorridor(Direction))
                            {
                                BuildCorridor();
                                BuildRoom();
                            }
                            break;
                    }
                }
            }

            return true;
        }


        #region room utilities

        /// <summary>
        /// Place a random sized room in the middle of the map
        /// </summary>
        private void PlaceStartRoom()
        {
            int x = MapSize.Width / 2;
            int y = MapSize.Height / 2;
            int width = rnd.Next(MinimumRoomSize.Width, MaximumRoomSize.Width);
            int height = rnd.Next(MinimumRoomSize.Height, MaximumRoomSize.Height);
            StartRoom = new StartRoom(x, y, height, width);
            CurrentRoom = StartRoom;
            BuildRoom();
        }

        /// <summary>
        /// Make a room off the last point of Corridor, using
        /// CorridorDirection as an indicator of how to offset the room.
        /// The potential room is stored in Room.
        /// </summary>
        private bool Room_AttemptBuildOnCorridor(Point pDirection)
        {
            int x = -1;
            int y = -1;
            int width = rnd.Next(MinimumRoomSize.Width, MaximumRoomSize.Width);
            int height = rnd.Next(MinimumRoomSize.Height, MaximumRoomSize.Height);

            //startbuilding room from this point
            Point lc = PotentialCorridor.Last();

            if (pDirection.X == 0) //north/south direction
            {
                x = rnd.Next(lc.X - width + 1, lc.X);

                if (pDirection.Y == 1)
                    y = lc.Y + 1;//south
                else
                    y = lc.Y - height - 1;//north


            }
            else if (pDirection.Y == 0)//east / west direction
            {
                y = rnd.Next(lc.Y - height + 1, lc.Y);

                if (pDirection.X == -1)//west
                    x = lc.X - width;
                else
                    x = lc.X + 1;//east
            }

            CurrentRoom = new GenericRoom(x, y, height, width);

            return VerifyRoom();
        }


        /// <summary>
        /// Randomly get a point on the edge of a randomly selected room
        /// </summary>
        /// <param name="Location">Out: Location of point on room edge</param>
        /// <param name="Location">Out: Direction of point</param>
        /// <returns>If Location is legal</returns>
        private void GetRoomEdge(out Point pLocation, out Point pDirection)
        {
            CurrentRoom = Rooms[rnd.Next(0, Rooms.Count())];

            //pick a random point within a room
            //the +1 / -1 on the values are to stop a corner from being chosen
            pLocation = new Point(rnd.Next(CurrentRoom.Left + 1, CurrentRoom.Right - 1),
                                  rnd.Next(CurrentRoom.Top + 1, CurrentRoom.Bottom - 1));


            //get a random direction
            pDirection = Directions[rnd.Next(0, Directions.GetLength(0))];

            do
            {
                //move in that direction
                pLocation.Offset(pDirection);

                if (!IsPointValid(pLocation.X, pLocation.Y))
                    return;

                //until we meet an empty cell
            } while (map[pLocation.X, pLocation.Y] != FILLED_CELL);

        }

        #endregion

        #region corridor utitlies

        /// <summary>
        /// Randomly get a point on an existing corridor
        /// </summary>
        /// <param name="Location">Out: location of point</param>
        /// <returns>Bool indicating success</returns>
        private void GetCorridorEdge(out Point pLocation, out Point pDirection)
        {
            List<Point> validdirections = new List<Point>();

            do
            {
                //the modifiers below prevent the first of last point being chosen
                pLocation = BuiltCorridors[rnd.Next(1, BuiltCorridors.Count - 1)];

                //attempt to locate all the empy map points around the location
                //using the directions to offset the randomly chosen point
                foreach (Point p in Directions)
                    if (IsPointValid(pLocation.X + p.X, pLocation.Y + p.Y))
                        if (map[pLocation.X + p.X, pLocation.Y + p.Y] == FILLED_CELL)
                            validdirections.Add(p);


            } while (validdirections.Count == 0);

            pDirection = validdirections[rnd.Next(0, validdirections.Count)];
            pLocation.Offset(pDirection);

        }

        /// <summary>
        /// Build the contents of PotentialCorridor, adding it's points to the builtCorridors
        /// list then empty
        /// </summary>
        private void BuildCorridor()
        {
            var first = PotentialCorridor[0];
            PotentialCorridor.RemoveAt(0);

            var last = PotentialCorridor[PotentialCorridor.Count - 1];
            PotentialCorridor.RemoveAt(PotentialCorridor.Count - 1);

            foreach (Point p in PotentialCorridor)
            {

                map[p.X, p.Y] = EMPTY_CELL;
                BuiltCorridors.Add(p);
            }

            map[first.X, first.Y] = 7;
            map[last.X, last.Y] = 7;

            BuiltCorridors.Add(first);
            BuiltCorridors.Add(last);

            PotentialCorridor.Clear();
        }

        /// <summary>
        /// Get a starting point for a corridor, randomly choosing between a room and a corridor.
        /// </summary>
        /// <param name="Location">Out: pLocation of point</param>
        /// <param name="Location">Out: pDirection of point</param>
        /// <returns>Bool indicating if location found is OK</returns>
        private bool GetCorridorStart(out Point pLocation, out Point pDirection)
        {
            PotentialCorridor = new List<Point>();

            if (BuiltCorridors.Count > 0)
            {
                if (rnd.Next(0, 100) >= BuildProb)
                    GetRoomEdge(out pLocation, out pDirection);
                else
                    GetCorridorEdge(out pLocation, out pDirection);
            }
            else//no corridors present, so build off a room
                GetRoomEdge(out pLocation, out pDirection);

            //finally check the point we've found
            return IsValidCorridorPoint(pLocation, pDirection) == CorridorItemHit.OK;

        }

        /// <summary>
        /// Attempt to make a corridor, storing it in the PotentialCorridor list
        /// </summary>
        /// <param name="pStart">Start point of corridor</param>
        /// <param name="pTurns">Number of turns to make</param>
        private CorridorItemHit MakeCorridorStraight(ref Point pStart, ref Point pDirection, int pTurns, bool pPreventBackTracking)
        {
            PotentialCorridor = new List<Point>();
            PotentialCorridor.Add(pStart);

            int corridorlength;
            Point startdirection = new Point(pDirection.X, pDirection.Y);
            CorridorItemHit outcome;

            while (pTurns > 0)
            {
                pTurns--;

                corridorlength = rnd.Next(MinimumCorridorLength, MaximumCorridorLength);
                //build corridor
                while (corridorlength > 0)
                {
                    corridorlength--;

                    //make a point and offset it
                    pStart.Offset(pDirection);

                    outcome = IsValidCorridorPoint(pStart, pDirection);
                    if (outcome != CorridorItemHit.OK)
                        return outcome;
                    else
                        PotentialCorridor.Add(pStart);
                }

                if (pTurns > 1)
                    if (!pPreventBackTracking)
                        pDirection = GetDirection(pDirection);
                    else
                        pDirection = GetDirection(pDirection, startdirection);
            }

            return CorridorItemHit.COMPLETED;
        }

        /// <summary>
        /// Test the provided point to see if it has empty cells on either side
        /// of it. This is to stop corridors being built adjacent to a room.
        /// </summary>
        /// <param name="pPoint">Point to test</param>
        /// <param name="pDirection">Direction it is moving in</param>
        /// <returns></returns>
        private CorridorItemHit IsValidCorridorPoint(Point pPoint, Point pDirection)
        {
            if (!IsPointValid(pPoint.X, pPoint.Y))//INVALID point hit, exit
                return CorridorItemHit.INVALID;
            else if (BuiltCorridors.Contains(pPoint))//in an existing corridor
                return CorridorItemHit.EXISTING_CORRIDOR;
            else if (PotentialCorridor.Contains(pPoint))//hit SELF
                return CorridorItemHit.SELF;
            else if (CurrentRoom != null && CurrentRoom.Contains(pPoint.X, pPoint.Y))//the corridors origin room has been reached, exit
                return CorridorItemHit.ORIGIN_ROOM;
            else
            {
                //is point in a room
                foreach (Room r in Rooms)
                    if (r.Contains(pPoint.X, pPoint.Y))
                        return CorridorItemHit.EXISTING_ROOM;
            }


            //using the property corridor space, check that number of cells on
            //either side of the point are empty
            foreach (int r in Enumerable.Range(-CorridorSpace, 2 * CorridorSpace + 1).ToList())
            {
                if (pDirection.X == 0)//north or south
                {
                    if (IsPointValid(pPoint.X + r, pPoint.Y))
                        if (map[pPoint.X + r, pPoint.Y] != FILLED_CELL)
                            return CorridorItemHit.TOO_CLOSE;
                }
                else if (pDirection.Y == 0)//east west
                {
                    if (IsPointValid(pPoint.X, pPoint.Y + r))
                        if (map[pPoint.X, pPoint.Y + r] != FILLED_CELL)
                            return CorridorItemHit.TOO_CLOSE;
                }

            }

            return CorridorItemHit.OK;
        }


        #endregion

        #region direction methods

        /// <summary>
        /// Get a random direction, excluding the opposite of the provided direction to
        /// prevent a corridor going back on it's Build
        /// </summary>
        /// <param name="dir">Current direction</param>
        /// <returns></returns>
        private Point GetDirection(Point pDir)
        {
            Point NewDir;
            do
            {
                NewDir = Directions[rnd.Next(0, Directions.GetLength(0))];
            } while (ReverseDirection(NewDir) == pDir);

            return NewDir;
        }

        /// <summary>
        /// Get a random direction, excluding the provided directions and the opposite of 
        /// the provided direction to prevent a corridor going back on it's SELF.
        /// 
        /// The parameter pDirExclude is the first direction chosen for a corridor, and
        /// to prevent it from being used will prevent a corridor from going back on 
        /// it'SELF
        /// </summary>
        /// <param name="dir">Current direction</param>
        /// <param name="pDirectionList">Direction to exclude</param>
        /// <param name="pDirExclude">Direction to exclude</param>
        /// <returns></returns>
        private Point GetDirection(Point pDir, Point pDirExclude)
        {
            Point NewDir;
            do
            {
                NewDir = Directions[rnd.Next(0, Directions.GetLength(0))];
            } while (
                        ReverseDirection(NewDir) == pDir
                         | ReverseDirection(NewDir) == pDirExclude
                    );


            return NewDir;
        }

        private Point ReverseDirection(Point pDir)
        {
            return new Point(-pDir.X, -pDir.Y);
        }

        #endregion

        #region room test

        /// <summary>
        /// Check if CurrentRoom can be built
        /// </summary>
        /// <returns>Bool indicating success</returns>
        private bool VerifyRoom()
        {
            //make it one bigger to ensure that testing gives it a border
            CurrentRoom.Inflate(RoomDistance, RoomDistance);

            //check it occupies legal, empty coordinates
            for (int x = CurrentRoom.Left; x <= CurrentRoom.Right; x++)
                for (int y = CurrentRoom.Top; y <= CurrentRoom.Bottom; y++)
                    if (!IsPointValid(x, y) || map[x, y] != FILLED_CELL)
                        return false;

            //check it doesn't encroach onto existing rooms
            foreach (Room r in Rooms)
                if (r.IntersectsWith(CurrentRoom))
                    return false;

            CurrentRoom.Inflate(-RoomDistance, -RoomDistance);

            //check the room is the specified distance away from corridors
            CurrentRoom.Inflate(CorridorDistance, CorridorDistance);

            foreach (Point p in BuiltCorridors)
                if (CurrentRoom.Contains(p.X, p.Y))
                    return false;

            CurrentRoom.Inflate(-CorridorDistance, -CorridorDistance);

            return true;
        }

        /// <summary>
        /// Add the global Room to the rooms collection and draw it on the map
        /// </summary>
        private void BuildRoom()
        {
            Rooms.Add(CurrentRoom);

            for (int x = CurrentRoom.Left; x <= CurrentRoom.Right; x++)
                for (int y = CurrentRoom.Top; y <= CurrentRoom.Bottom; y++)
                    map[x, y] = EMPTY_CELL;

        }

        #endregion

        #region Map Utilities

        /// <summary>
        /// Check if the point falls within the map array range
        /// </summary>
        /// <param name="x">x to test</param>
        /// <param name="y">y to test</param>
        /// <returns>Is point with map array?</returns>
        private bool IsPointValid(int x, int y)
        {
            return x >= 0 & x < map.GetLength(0) & y >= 0 & y < map.GetLength(1);
        }

        #endregion
    }
}