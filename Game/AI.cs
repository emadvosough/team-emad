using System;
using System.Collections.Generic;

using KoalaTeam.Chillin.Client;
using KS;
using KS.Commands;
using KS.Models;

using KSObject = KS.KSObject;

namespace Game
{
    public class Max
    {

        public Max(Int64[] arr, ref int direct)//to choose the best way
        {
            Int64 max = arr[0];
            Int64 IsSame = arr[0];//if all elements are same 

            foreach (Int64 i in arr)
            {

                if (i > max)
                {
                    max = i;
                }
            }

            for (int i = 0; i < 4; ++i)
            {
                if (arr[i] == max)
                {
                    direct = i;

                }
            }

        }

    }
    public class AI : RealtimeAI<World, KSObject>
    {
        //variables
        int path = 0;
        int wallBreakerleft;
        int wallBreakerCool;
        int health;


        private readonly Random random = new Random();
        private int stage = 0;
        int side;


        public AI(World world) : base(world)
        {
        }
        //put 1 in for blue and 0 for yellow side;
        //put how many more movement predict in movement;
        //X,Y of the agent
        //at first put 1 int direct;
        public void way(ref Int64 direct, int movement, EDirection direction, int X, int Y)
        {
            int cycleaPre;
            if (CurrentCycle < 30)
            {
                cycleaPre = ((CurrentCycle % 9) / 3) + 3;
            }
            else
            {
                cycleaPre = 6;
            }


            if (movement < cycleaPre)//how many more movment predict 
            {

                if (side == 0)//yellow agent
                {
                    if (direction == (EDirection)0)//up 
                    {
                        if (World.Board[Y][X + 1] != (ECell)1)// to not scan areawalls while going right direction;
                        {
                            if (World.Board[Y][X + 1] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y][X + 1] == (ECell)2) //blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct -= 8;
                                }

                            }
                            else if (World.Board[Y][X + 1] == (ECell)3)//yellowwall
                            {
                                direct -= 3;

                            }

                        }

                        if (World.Board[Y][X - 1] != (ECell)1)// to not scan areawalls while going left direction;
                        {
                            if (World.Board[Y][X - 1] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y][X - 1] == (ECell)2)//blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 8;
                                }

                            }
                            else if (World.Board[Y][X - 1] == (ECell)3)//yellowwall
                            {
                                direct -= 5;

                            }
                        }


                        if (World.Board[Y - 1][X] != (ECell)1)// to not scan areawalls while going up direction;
                        {
                            if (World.Board[Y - 1][X] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y - 1][X] == (ECell)2)//blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 5;
                                }
                                else
                                {
                                    direct = direct - 7;
                                }

                            }
                            else if (World.Board[Y - 1][X] == (ECell)3)//yellowwall
                            {
                                direct -= 5;

                            }

                        }

                    }

                    else if (direction == (EDirection)1)//right 
                    {
                        if (World.Board[Y][X + 1] != (ECell)1) // to not scan areawalls while going right direction;
                        {
                            if (World.Board[Y][X + 1] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y][X + 1] == (ECell)2)//blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 7;
                                }

                            }
                            else if (World.Board[Y][X + 1] == (ECell)3) //yellowwall
                            {
                                direct -= 4;

                            }

                        }

                        if (World.Board[Y + 1][X] != (ECell)1) // to not scan areawalls while going down direction;
                        {
                            if (World.Board[Y + 1][X] == (ECell)0) //empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y + 1][X] == (ECell)2)//blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 3;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y + 1][X] == (ECell)3)//yellowwall
                            {
                                direct -= 4;

                            }
                        }

                        if (World.Board[Y - 1][X] != (ECell)1) // to not scan areawalls while going up direction;
                        {
                            if (World.Board[Y - 1][X] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y - 1][X] == (ECell)2)//blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y - 1][X] == (ECell)3)//yellowwall
                            {
                                direct -= 4;

                            }

                        }

                    }
                    else if (direction == (EDirection)2)//down 
                    {
                        if (World.Board[Y + 1][X] != (ECell)1) // to not scan areawalls while going down direction;
                        {
                            if (World.Board[Y + 1][X] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y + 1][X] == (ECell)2)//blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 3;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y + 1][X] == (ECell)3)//yellowwall
                            {
                                direct -= 4;

                            }

                        }

                        if (World.Board[Y][X - 1] != (ECell)1)// to not scan areawalls while going left direction;
                        {
                            if (World.Board[Y][X - 1] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y][X - 1] == (ECell)2)//blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 7;
                                }

                            }
                            else if (World.Board[Y][X - 1] == (ECell)3)//yellowwall
                            {
                                direct -= 4;

                            }
                        }

                        if (World.Board[Y][X + 1] != (ECell)1) // to not scan areawalls while going right direction;
                        {
                            if (World.Board[Y][X + 1] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y][X + 1] == (ECell)2)//blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 7;
                                }

                            }
                            else if (World.Board[Y][X + 1] == (ECell)3)//yellowwall
                            {
                                direct -= 4;

                            }

                        }

                    }
                    else if (direction == (EDirection)3)//left
                    {
                        if (World.Board[Y + 1][X] != (ECell)1) // to not scan areawalls while going down direction;
                        {
                            if (World.Board[Y + 1][X] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y + 1][X] == (ECell)2)//blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 7;
                                }

                            }
                            else if (World.Board[Y + 1][X] == (ECell)3)//yellowwall
                            {
                                direct -= 4;

                            }

                        }

                        if (World.Board[Y][X - 1] != (ECell)1)// to not scan areawalls while going left direction;
                        {
                            if (World.Board[Y][X - 1] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y][X - 1] == (ECell)2)//blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y][X - 1] == (ECell)3)//yellowwall
                            {
                                direct -= 5;

                            }
                        }

                        if (World.Board[Y - 1][X] != (ECell)1) // to not scan areawalls while going up direction;
                        {
                            if (World.Board[Y - 1][X] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y - 1][X] == (ECell)2)//blue wall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y - 1][X] == (ECell)3)//yellowwall
                            {
                                direct -= 5;

                            }

                        }

                    }

                }
                else if (side == 1)//Blue agent
                {
                    if (direction == (EDirection)0)//up 
                    {
                        if (World.Board[Y][X + 1] != (ECell)1)// to not scan areawalls while going right direction;
                        {
                            if (World.Board[Y][X + 1] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y][X + 1] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 7;
                                }

                            }
                            else if (World.Board[Y][X + 1] == (ECell)2)//blue wall
                            {
                                direct -= 4;

                            }

                        }

                        if (World.Board[Y][X - 1] != (ECell)1)// to not scan areawalls while going left direction;
                        {
                            if (World.Board[Y][X - 1] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y][X - 1] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 7;
                                }

                            }
                            else if (World.Board[Y][X - 1] == (ECell)2)//blue wall
                            {
                                direct -= 4;

                            }
                        }

                        if (World.Board[Y - 1][X] != (ECell)1)// to not scan areawalls while going up direction;
                        {
                            if (World.Board[Y - 1][X] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y - 1][X] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y - 1][X] == (ECell)2)//blue wall
                            {
                                direct -= 4;

                            }

                        }

                    }
                    else if (direction == (EDirection)1)//right 
                    {
                        if (World.Board[Y][X + 1] != (ECell)1) // to not scan areawalls while going right direction;
                        {
                            if (World.Board[Y][X + 1] == (ECell)0)//empty
                            {
                                direct += 8;


                            }
                            else if (World.Board[Y][X + 1] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y][X + 1] == (ECell)2) //blue wall
                            {
                                direct -= 4;

                            }

                        }

                        if (World.Board[Y + 1][X] != (ECell)1) // to not scan areawalls while going down direction;
                        {
                            if (World.Board[Y + 1][X] == (ECell)0) //empty
                            {
                                direct += 7;


                            }
                            else if (World.Board[Y + 1][X] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0)
                                    )
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y + 1][X] == (ECell)2)//blue wall
                            {
                                direct -= 4;

                            }
                        }

                        if (World.Board[Y - 1][X] != (ECell)1) // to not scan areawalls while going up direction;
                        {
                            if (World.Board[Y - 1][X] == (ECell)0)//empty
                            {
                                direct += 7;


                            }
                            else if (World.Board[Y - 1][X] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 7;
                                }

                            }
                            else if (World.Board[Y - 1][X] == (ECell)2)//blue wall
                            {
                                direct -= 4;

                            }

                        }

                    }
                    else if (direction == (EDirection)2)//down 
                    {
                        if (World.Board[Y + 1][X] != (ECell)1) // to not scan areawalls while going down direction;
                        {
                            if (World.Board[Y + 1][X] == (ECell)0)//empty
                            {
                                direct += 7;


                            }
                            else if (World.Board[Y + 1][X] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y + 1][X] == (ECell)2)//blue wall
                            {
                                direct -= 4;

                            }

                        }

                        if (World.Board[Y][X - 1] != (ECell)1)// to not scan areawalls while going left direction;
                        {
                            if (World.Board[Y][X - 1] == (ECell)0)//empty
                            {
                                direct += 7;


                            }
                            else if (World.Board[Y][X - 1] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y][X - 1] == (ECell)2)//blue wall
                            {
                                direct -= 4;

                            }
                        }

                        if (World.Board[Y][X + 1] != (ECell)1) // to not scan areawalls while going right direction;
                        {
                            if (World.Board[Y][X + 1] == (ECell)0)//empty
                            {
                                direct += 7;


                            }
                            else if (World.Board[Y][X + 1] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 5;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y][X + 1] == (ECell)2)//blue wall
                            {
                                direct -= 4;

                            }

                        }

                    }
                    else if (direction == (EDirection)3)//left
                    {
                        if (World.Board[Y + 1][X] != (ECell)1) // to not scan areawalls while going down direction;
                        {
                            if (World.Board[Y + 1][X] == (ECell)0)//empty
                            {
                                direct += 7;


                            }
                            else if (World.Board[Y + 1][X] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement == 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y + 1][X] == (ECell)2)//blue wall
                            {
                                direct -= 4;

                            }

                        }

                        if (World.Board[Y][X - 1] != (ECell)1)// to not scan areawalls while going left direction;
                        {
                            if (World.Board[Y][X - 1] == (ECell)0)//empty
                            {
                                direct += 7;


                            }
                            else if (World.Board[Y][X - 1] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 6;
                                }

                            }
                            else if (World.Board[Y][X - 1] == (ECell)2)//blue wall
                            {
                                direct -= 4;

                            }
                        }

                        if (World.Board[Y - 1][X] != (ECell)1) // to not scan areawalls while going up direction;
                        {
                            if (World.Board[Y - 1][X] == (ECell)0)//empty
                            {
                                direct += 7;


                            }
                            else if (World.Board[Y - 1][X] == (ECell)3)//yellowwall
                            {
                                if (wallBreakerleft - movement > 1 || (wallBreakerCool - movement <= 0 && wallBreakerleft == 0))
                                {
                                    direct += 4;
                                }
                                else
                                {
                                    direct = direct - 7;
                                }

                            }
                            else if (World.Board[Y - 1][X] == (ECell)2)//blue wall
                            {
                                direct -= 4;

                            }

                        }

                    }

                }
                ++movement;
                try
                {
                    if (World.Agents[this.MySide].Direction == (EDirection)0)//up direction
                    {
                        if (World.Board[Y - 1][X] != (ECell)1)//to not scan areawall
                            way(ref direct, movement, (EDirection)0, X, Y - 1);
                        if (World.Board[Y][X - 1] != (ECell)1)
                            way(ref direct, movement, (EDirection)3, X - 1, Y);
                        if (World.Board[Y][X + 1] != (ECell)1)//to not scan areawall
                            way(ref direct, movement, (EDirection)1, X, Y + 1);

                    }
                    else if (World.Agents[this.MySide].Direction == (EDirection)1)//right direction
                    {
                        if (World.Board[Y - 1][X] != (ECell)1)//to not scan areawall
                            way(ref direct, movement, (EDirection)0, X, Y - 1);
                        if (World.Board[Y + 1][X] != (ECell)1)
                            way(ref direct, movement, (EDirection)2, X, Y + 1);
                        if (World.Board[Y][X + 1] != (ECell)1)//to not scan areawall
                            way(ref direct, movement, (EDirection)1, X + 1, Y);

                    }
                    else if (World.Agents[this.MySide].Direction == (EDirection)2)//down direction
                    {
                        if (World.Board[Y][X - 1] != (ECell)1)//to not scan areawall
                            way(ref direct, movement, (EDirection)3, X - 1, Y);
                        if (World.Board[Y + 1][X] != (ECell)1)
                            way(ref direct, movement, (EDirection)2, X, Y + 1);
                        if (World.Board[Y][X + 1] != (ECell)1)//to not scan areawall
                            way(ref direct, movement, (EDirection)1, X + 1, Y);

                    }
                    else if (World.Agents[this.MySide].Direction == (EDirection)3)//left direction
                    {
                        if (World.Board[Y][X - 1] != (ECell)1)//to not scan areawall
                            way(ref direct, movement, (EDirection)3, X - 1, Y);
                        if (World.Board[Y + 1][X] != (ECell)1)
                            way(ref direct, movement, (EDirection)2, X, Y + 1);
                        if (World.Board[Y - 1][X] != (ECell)1) //to not scan areawall
                            way(ref direct, movement, (EDirection)0, X, Y - 1);

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }

        public override void Initialize()
        {
            Console.WriteLine("initialize");Console.Write(World.Agents[this.MySide].Name());
            if (World.Agents[this.MySide].Name() == "Blue")
            {
                side = 1;

            }
            else
            {
                side = 0;
            }

        }

        public override void Decide()
        {
            Console.Write(World.Agents[this.MySide].Name());

            if (World.Agents[this.MySide].Name() == "Blue")
            {
                side = 1;

            }
            else
            {
                side = 0;
            }
            wallBreakerCool = World.Agents[this.MySide].WallBreakerCooldown ?? default(int);
            wallBreakerleft = World.Agents[this.MySide].WallBreakerRemTime ?? default(int);
            health = World.Agents[this.MySide].Health ?? default(int);

            Console.WriteLine("decide");
            //"world.board.count"Y of board;
            //"world.board[0].count"X of board;
            //"string what is in an ECell = Enum.GetName(typeof(ECell),this.World.Board[Y][X])

            int? myAgentXQ = World.Agents[this.MySide].Position.X;
            int myAgentX = myAgentXQ ?? default(int);
            int? myAgentYQ = World.Agents[this.MySide].Position.Y;
            int myAgentY = myAgentYQ ?? default(int);//Y of my agent;
            Int64[] direct = new Int64[] { 0, 0, 0, 0 };
            Console.WriteLine(myAgentY + "#" + myAgentX);
            Console.Write("%%" + Enum.GetName(typeof(EDirection), World.Agents[this.MySide].Direction) + "%% \n");
            if (Enum.GetName(typeof(ECell), this.World.Board[myAgentY][myAgentX - 1]) == "AreaWall")
            {
                direct[3] = -99999999;
            }
            if (Enum.GetName(typeof(ECell), this.World.Board[myAgentY][myAgentX + 1]) == "AreaWall")
            {
                direct[1] = -99999999;
            }
            if (Enum.GetName(typeof(ECell), this.World.Board[myAgentY - 1][myAgentX]) == "AreaWall")
            {
                direct[0] = -99999999;
            }
            if (Enum.GetName(typeof(ECell), this.World.Board[myAgentY + 1][myAgentX]) == "AreaWall")
            {
                direct[2] = -99999999;
            }
            //NOT going backward;
            if (World.Agents[this.MySide].Direction == (EDirection)0)
            {
                direct[2] = -999999999;
            }
            if (World.Agents[this.MySide].Direction == (EDirection)1)
            {
                direct[3] = -999999999;
            }
            if (World.Agents[this.MySide].Direction == (EDirection)2)
            {
                direct[0] = -999999999;
            }
            if (World.Agents[this.MySide].Direction == (EDirection)3)
            {
                direct[1] = -999999999;
            }
            int movement = 0;

            Int64 help = 0;//for sending to way function;

            //change direction before losing health;                 


            if (World.Agents[this.MySide].Direction == (EDirection)0)//up direction
            {
                if (World.Board[myAgentY - 1][myAgentX] != (ECell)1)//to not scan areawall
                    way(ref help, movement, (EDirection)0, myAgentX, myAgentY - 1);
                direct[0] += help;
                help = 0;

                if (World.Board[myAgentY][myAgentX - 1] != (ECell)1)
                    way(ref help, movement, (EDirection)3, myAgentX - 1, myAgentY);
                direct[3] += help;
                help = 0;
                if (World.Board[myAgentY][myAgentX + 1] != (ECell)1)//to not scan areawall
                    way(ref help, movement, (EDirection)1, myAgentX + 1, myAgentY);
                direct[1] += help;
                help = 0;
            }
            else if (World.Agents[this.MySide].Direction == (EDirection)1)//right direction
            {
                if (World.Board[myAgentY - 1][myAgentX] != (ECell)1)//to not scan areawall
                    way(ref help, movement, (EDirection)0, myAgentX, myAgentY - 1);
                direct[0] += help;
                help = 0;

                if (World.Board[myAgentY + 1][myAgentX] != (ECell)1)
                    way(ref help, movement, (EDirection)2, myAgentX, myAgentY + 1);
                direct[2] += help;
                help = 0;
                if (World.Board[myAgentY][myAgentX + 1] != (ECell)1)//to not scan areawall
                    way(ref help, movement, (EDirection)1, myAgentX + 1, myAgentY);
                direct[1] += help;
                help = 0;
            }
            else if (World.Agents[this.MySide].Direction == (EDirection)2)//down direction
            {
                if (World.Board[myAgentY][myAgentX - 1] != (ECell)1)//to not scan areawall
                    way(ref help, movement, (EDirection)3, myAgentX - 1, myAgentY);
                direct[3] *= help;
                help = 0;
                if (World.Board[myAgentY + 1][myAgentX] != (ECell)1)
                    way(ref help, movement, (EDirection)2, myAgentX, myAgentY + 1);
                direct[2] += help;
                help = 0;
                if (World.Board[myAgentY][myAgentX + 1] != (ECell)1)//to not scan areawall
                    way(ref help, movement, (EDirection)1, myAgentX + 1, myAgentY);
                direct[1] += help;
                help = 0;
            }
            else if (World.Agents[this.MySide].Direction == (EDirection)3)//left direction
            {
                if (World.Board[myAgentY][myAgentX - 1] != (ECell)1)//to not scan areawall
                    way(ref help, movement, (EDirection)3, myAgentX - 1, myAgentY);
                direct[3] += help;
                help = 0;
                if (World.Board[myAgentY + 1][myAgentX] != (ECell)1)
                    way(ref help, movement, (EDirection)2, myAgentX, myAgentY + 1);
                direct[2] += help;
                help = 0;
                if (World.Board[myAgentY - 1][myAgentX] != (ECell)1) //to not scan areawall
                    way(ref help, movement, (EDirection)0, myAgentX, myAgentY - 1);
                direct[0] += help;
                help = 0;
            }
            if (Enum.GetName(typeof(ECell), this.World.Board[myAgentY][myAgentX - 1]) == "Empty")
            {

                if (World.Agents[this.MySide].Direction != EDirection.Left)
                {
                    direct[3]++;
                    direct[3] += 20;
                }




            }
            if (Enum.GetName(typeof(ECell), this.World.Board[myAgentY][myAgentX + 1]) == "Empty")
            {

                if (World.Agents[this.MySide].Direction != EDirection.Right)
                {
                    direct[1]++;
                    direct[1] += 20;
                }



            }

            if (Enum.GetName(typeof(ECell), this.World.Board[myAgentY - 1][myAgentX]) == "Empty")
            {

                if (World.Agents[this.MySide].Direction != EDirection.Up)
                {
                    direct[0]++;
                    direct[0] += 20;
                }


            }


            if (Enum.GetName(typeof(ECell), this.World.Board[myAgentY + 1][myAgentX]) == "Empty")
            {

                if (World.Agents[this.MySide].Direction != EDirection.Down)
                {
                    direct[2]++;
                    direct[2] += 20;
                }


            }


            path = 0;

            Max a = new Max(direct, ref path);
            if (path == -1)//for the case of four zero 
            {
                if (World.Agents[this.MySide].Direction == (EDirection)0)//up direction
                {
                    if (World.Board[myAgentY][myAgentX - 1] != (ECell)1)//to not scan areawall
                    {
                        ++direct[3];
                        if (World.Board[myAgentY][myAgentX - 1] == (ECell)0) direct[3] += 4;//for empty rooms
                    }
                    if (World.Board[myAgentY][myAgentX + 1] != (ECell)1)//to not scan areawall
                    {
                        ++direct[1];
                        if (World.Board[myAgentY][myAgentX + 1] == (ECell)0) direct[1] += 4;//for empty rooms

                    }
                    if (World.Board[myAgentY - 1][myAgentX] != (ECell)1)//to not scan areawall
                    {
                        ++direct[0];
                        if (World.Board[myAgentY - 1][myAgentX] == (ECell)0) direct[0] += 4;//for empty rooms
                    }
                }
                if (World.Agents[this.MySide].Direction == (EDirection)1)//right direction
                {
                    if (World.Board[myAgentY + 1][myAgentX] != (ECell)1)//to not scan areawall
                    {
                        ++direct[2];
                        if (World.Board[myAgentY + 1][myAgentX] == (ECell)0) direct[2] += 4;//for empty rooms
                    }
                    if (World.Board[myAgentY][myAgentX + 1] != (ECell)1)//to not scan areawall
                    {
                        ++direct[1];
                        if (World.Board[myAgentY][myAgentX + 1] == (ECell)0) direct[1] += 4;//for empty rooms
                    }
                    if (World.Board[myAgentY - 1][myAgentX] != (ECell)1)//to not scan areawall
                    {
                        ++direct[0];
                        if (World.Board[myAgentY - 1][myAgentX] == (ECell)0) direct[0] += 4;//for empty rooms
                    }
                }
                if (World.Agents[this.MySide].Direction == (EDirection)2)//down direction
                {
                    if (World.Board[myAgentY][myAgentX - 1] != (ECell)1)//to not scan areawall
                    {
                        ++direct[3];
                        if (World.Board[myAgentY][myAgentX - 1] == (ECell)0) direct[3] += 4;//for empty rooms
                    }
                    if (World.Board[myAgentY + 1][myAgentX] != (ECell)1)//to not scan areawall
                    {
                        ++direct[2];
                        if (World.Board[myAgentY + 1][myAgentX] == (ECell)0) direct[2] += 4;//for empty rooms
                    }
                    if (World.Board[myAgentY][myAgentX + 1] != (ECell)1)//to not scan areawall
                    {
                        ++direct[1];
                        if (World.Board[myAgentY][myAgentX + 1] == (ECell)0) direct[1] += 4;//for empty rooms
                    }
                }
                if (World.Agents[this.MySide].Direction == (EDirection)3)//left direction
                {
                    if (World.Board[myAgentY][myAgentX - 1] != (ECell)1)//to not scan areawall
                    {
                        ++direct[3];
                        if (World.Board[myAgentY][myAgentX - 1] == (ECell)0) direct[3] += 4;//for empty rooms
                    }
                    if (World.Board[myAgentY - 1][myAgentX] != (ECell)1)//to not scan areawall
                    {
                        ++direct[0];
                        if (World.Board[myAgentY - 1][myAgentX] == (ECell)0) direct[0] += 4;//for empty rooms
                    }
                    if (World.Board[myAgentY + 1][myAgentX] != (ECell)1) //to not scan areawall
                    {
                        ++direct[2];
                        if (World.Board[myAgentY + 1][myAgentX] == (ECell)0) direct[2] += 4;//for empty rooms
                    }
                }
            }
            a = new Max(direct, ref path);
            ChangeDirection((EDirection)path);

            Console.Write("%%" + Enum.GetName(typeof(EDirection), World.Agents[this.MySide].Direction) + "%" + direct[0] + "% " + direct[1] + "% " + direct[2] + "% " + "% " + direct[3] + "% " + "\n");


            if (World.Agents[this.MySide].Direction == EDirection.Up)
            {
                if (World.Board[myAgentY - 1][myAgentX] == ECell.BlueWall || World.Board[myAgentY - 1][myAgentX] == ECell.YellowWall)
                {
                    ActivateWallBreaker();
                }

            }

            else if (World.Agents[this.MySide].Direction == (EDirection.Right))
            {
                if (World.Board[myAgentY][myAgentX + 1] == ECell.BlueWall || World.Board[myAgentY][myAgentX + 1] == ECell.YellowWall)
                {
                    ActivateWallBreaker();
                }

            }
            else if (World.Agents[this.MySide].Direction == EDirection.Down)
            {
                if (World.Board[myAgentY + 1][myAgentX] == ECell.BlueWall || World.Board[myAgentY + 1][myAgentX] == ECell.YellowWall)
                {
                    ActivateWallBreaker();
                }

            }
            else if (World.Agents[this.MySide].Direction == EDirection.Left)
            {
                if (World.Board[myAgentY][myAgentX - 1] == ECell.BlueWall || World.Board[myAgentY][myAgentX - 1] == ECell.YellowWall)
                {
                    ActivateWallBreaker();
                }

            }


        }


        public void ChangeDirection(EDirection direction)
        {
            this.SendCommand(new ChangeDirection() { Direction = (ECommandDirection?)direction });
        }

        public void ActivateWallBreaker()
        {
            this.SendCommand(new ActivateWallBreaker());
        }
    }
}
