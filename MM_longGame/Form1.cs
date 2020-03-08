using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MM_longGame
{
    public class cActor
    {
       
        public Rectangle RcSrc;
        public Rectangle RcDst;
        public List<Bitmap> img=new List<Bitmap>();
        public int j=0;
        public bool isExsit = true;
        public int bullct = 0;
        public int deathCt = 0;
        public bool up = false;
        public int x, y;
        public int w, h;
        public int index = 0;
    }
    public class cActor2
    {
        public List<cActor> L = new List<cActor>();
        public List<cActor> bullet = new List<cActor>();
        public int index = 0;
    }
    public partial class Form1 : Form
    {
        List<cActor> menuSt = new List<cActor>();
        List<cActor> SelectHero = new List<cActor>();
        List<cActor> Hero = new List<cActor>();
        List<cActor> bullet = new List<cActor>();
        List<cActor> background = new List<cActor>();
        List<cActor> L = new List<cActor>();
        List<cActor> car = new List<cActor>();
        List<cActor> boss = new List<cActor>();
        List<cActor> bossFire = new List<cActor>();
        List<cActor> bossFire2 = new List<cActor>();
        List<cActor2> En = new List<cActor2>();
        List<cActor2> En2 = new List<cActor2>();
        List<cActor2> En3 = new List<cActor2>();
        List<cActor2> prisoner = new List<cActor2>();
        Bitmap off;
        Timer t = new Timer();
        Timer tenm = new Timer();
        Timer menub = new Timer();
        // ct -> count for idle anam
        int ct = 0;
        /*
         * charIndex=0 -> IDLE
         * charIndex=1 -> run-left side
         * charIndex=2 -> run-right side
         * charIndex=3 -> jump
         * charIndex=4 -> knife
         */
        int charIndex = 9;
        int fienm = 0;
        int jumpFlag = 0, jumpCt = 0;
        int jumpinc = 30;
        bool isLeft = false;
        bool init = false;
        bool isShot = false;
        // for subBack
        int dx = 320, dy = -200;
        int flag = 0;
        bool isHit=false, isHit2 = false, isHit3=false;
        bool isSh = false, isSh2=false;
        int pos = 0;
        int enmindex1 = 0, enmindex2 = 0, enmindex3=-1, enmindex4=0;
        Random rr = new Random();
        bool run = false;
        bool jumpLeft = false;
        bool jum = false;
        string folder = "hero1";
        bool heroDeath = false;
        bool cam = false;
        int camct = 0;
        bool isUP = false;
        bool menu = true;
        bool select = false;
        bool slidedown = false;
        int down = 590;
        bool end = false;
        bool issit = false;
        int eneCt = 0;
        int bossCt = 0;
        int bossHit = 0;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            t.Tick += T_Tick;
            tenm.Tick += Tenm_Tick;
            menub.Tick += Menub_Tick;
            menub.Start();
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            for(int i=0;i<menuSt.Count;i++)
            {
                if (e.X >= menuSt[i].x
                    && e.X <= menuSt[i].x + 300
                    && e.Y >= menuSt[i].y
                    && e.Y <= menuSt[i].y + 100
                    )
                {
                    menuSt[i].j = 1;
                }
                else
                {
                    menuSt[i].j = 0;
                }
            }

            for (int i = 0; i < SelectHero.Count; i++)
            {
                if (e.X    >= SelectHero[i].x
                    && e.X <= SelectHero[i].x + 320
                    && e.Y >= SelectHero[i].y
                    && e.Y <= SelectHero[i].y + 480
                    )
                {
                    SelectHero[i].j = 1;
                }
                else
                {
                    SelectHero[i].j = 0;
                }
            }
        }

        private void Menub_Tick(object sender, EventArgs e)
        {
            if(!menu)
            {
                t.Start();
                tenm.Start();
                menub.Stop();
            }
            if(slidedown)
            {
                
                if (dy <= 280)
                    dy += 20;
                else
                {
                    menu = false;
                }
            }
            DrawDubb(this.CreateGraphics());
        }
        public void en3Check()
        {
            for (int i = 0; i < En3.Count; i++)
            {
                if (Hero[charIndex].RcDst.X + 400 >= En3[i].L[En3[i].index].RcDst.X
                            && Hero[charIndex].RcDst.X <= En3[i].L[En3[i].index].RcDst.Right

                            )
                {

                    if (En3[i].L[En3[i].index].isExsit)
                        En3[i].index = 0;
                    else
                        En3[i].index = 1;
                }
                else
                {
                    if (En3[i].L[En3[i].index].isExsit)
                    {
                        En3[i].index = 2;
                        for (int k = 0; k < En[i].L.Count; k++)
                        {
                            En3[i].L[k].RcDst.X -= 11;
                        }
                    }

                }
            }

            for (int i = 0; i < En3.Count; i++)
            {
                if (Hero[charIndex].RcDst.X+200 >= En3[i].L[En3[i].index].RcDst.X
                            && Hero[charIndex].RcDst.X <= En3[i].L[En3[i].index].RcDst.Right
                             && En3[i].L[En3[i].index].isExsit
                            )
                {

                    isHit3 = true;
                    En3[i].index = 3;
                    for (int q = 0; q < Hero.Count; q++)
                    {
                        Hero[q].deathCt+=10;
                    }
                }
               
              

            }
          

        }
        public void bossCheck()
        {
            for (int k = 0; k < bullet.Count; k++)
            {
                for (int i = 0; i < boss.Count; i++)
                {
                    if (bullet[k].RcDst.X>= boss[i].RcDst.X
                                && bullet[k].RcDst.X <= boss[i].RcDst.Right
                                && bullet[k].RcDst.Y <= boss[i].RcDst.Bottom
                                && bullet[k].RcDst.Y >= boss[i].RcDst.Bottom-200
                                )
                    {
                        bossHit++;
                        boss[0].index = 2;
                        break;
                        
                    }
                    else
                    {
                        if (bossHit > 20)
                        {

                            boss[0].index = 1;
 
                        }
                        else
                        {
                            boss[0].index = 0;
                        }
                      
                    }

                }
            }
           

        }
        private void Tenm_Tick(object sender, EventArgs e)
        {
            //---------E-S-L-A-M------enm-----A-Y-M-A-N------------
            for (int i = 0; i < En.Count; i++)
            {


                En[i].L[En[i].index].j++;


                if (En[i].L[En[i].index].j >= En[i].L[En[i].index].img.Count - 1 && En[i].L[En[i].index].isExsit != false)
                    En[i].L[En[i].index].j = 0;

                if (En[i].L[1].j >= En[i].L[1].img.Count - 1)
                    En[i].L[1].j = En[i].L[1].img.Count - 1;


            }
            for (int k = 0; k < En.Count; k++)
            {
                for (int i = 0; i < En[k].L.Count; i++)
                {
                    if (En[k].L[i].isExsit == true && Hero[charIndex].isExsit)
                    {
                        check();
                        if (enmindex1 != k)
                        {
                            En[k].index = 2;
                            En[k].L[i].RcDst.X -= 11;
                        }
                        else
                        {
                            En[k].index = 3;
                            if (isHit)
                            {
                                for (int q = 0; q < Hero.Count; q++)
                                {
                                    Hero[q].deathCt++;
                                }
                            }
                        }

                    }



                    if (En[k].L[En[k].index].j >= En[k].L[En[k].index].img.Count - 1 && En[k].L[En[k].index].isExsit != false)
                        En[k].L[En[k].index].j = 0;
                    isHit = false;
                }



            }

            //-------------------------- en2-------------------------
            for (int i = 0; i < En2.Count; i++)
            {


                En2[i].L[En2[i].index].j++;


                if (En2[i].L[En2[i].index].j >= En[i].L[En[i].index].img.Count && En2[i].L[En2[i].index].isExsit != false && cam)
                    En[i].L[En[i].index].j = 0;


            }
            for (int k = 0; k < En2.Count; k++)
            {
                for (int i = 0; i < En2[k].L.Count; i++)
                {
                    if (En2[k].L[i].isExsit == true && Hero[charIndex].isExsit)
                    {
                        check2();
                        if (enmindex3 != k)
                        {

                            En2[k].L[i].RcDst.X -= 20;
                        }
                        else
                        {


                            if (!cam)
                            {
                                En2[k].index = 1;
                                if (En2[k].L[En2[k].index].j >= En2[k].L[En2[k].index].img.Count && En2[k].L[En2[k].index].isExsit != false)
                                    En2[k].L[En2[k].index].j = En2[k].L[En2[k].index].img.Count - 1;
                                else
                                    cam = true;
                            }
                            if (ct % 12 == 0 && cam)
                            {
                                En2[k].index = 2;
                                cActor pnn = new cActor();
                                Bitmap pv = new Bitmap("En/bullet.png");
                                pv.MakeTransparent(pv.GetPixel(0, 0));
                                pnn.img.Add(pv);
                                pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);
                                pnn.RcDst = new Rectangle(En2[k].L[1].RcDst.X - 10, En2[k].L[1].RcDst.Y + 120, 50, 45);
                                En2[k].bullet.Add(pnn);
                            }


                            if (En2[k].L[En2[k].index].j >= En2[k].L[En2[k].index].img.Count && En2[k].L[En2[k].index].isExsit != false && En2[k].index != 1 && cam)
                                En2[k].L[En2[k].index].j = 0;


                        }

                    }
                    else
                    {
                        if (En2[k].L[i].isExsit == true == false)
                        {
                            En2[enmindex4].index = 3;

                            if (En2[k].L[En2[k].index].j >= En2[k].L[En2[k].index].img.Count && En2[k].L[En2[k].index].isExsit == false)
                                En2[k].L[En2[k].index].j = En2[k].L[En2[k].index].img.Count - 1;
                        }
                    }

                    if (En2[k].L[En2[k].index].j >= En2[k].L[En2[k].index].img.Count && En2[k].L[En2[k].index].isExsit != false && isHit2 == false)
                        En2[k].L[En2[k].index].j = 0;
                    isHit2 = false;
                }

                for (int t = 0; t < En2[k].bullet.Count; t++)
                {
                    check2();

                    En2[k].bullet[t].RcDst.X -= 50;
                    
                    
                        
                        
                   if (Hero[charIndex].RcDst.X+80 >= En2[k].bullet[t].RcDst.X
                               && Hero[charIndex].RcDst.X <= En2[k].bullet[t].RcDst.Right
                               && Hero[charIndex].RcDst.Y >= En2[k].bullet[t].RcDst.Y
                               && Hero[charIndex].RcDst.Y <= En2[k].bullet[t].RcDst.Bottom
                               && charIndex!=5
                               )
                   {

                        En2[k].bullet.RemoveAt(t);
                       
                        for (int q = 0; q < Hero.Count; q++)
                        {
                            Hero[q].deathCt+=10;
                        }


                    }


                    if (isSh2)
                    {
                        try
                        {
                            bullet.RemoveAt(pos);
                        }
                        catch (Exception ex) {
                        }

                        if (camct == 5)
                        {
                            for (int o = 0; o < En2[enmindex4].L.Count; o++)
                            {
                                En2[enmindex4].L[o].isExsit = false;
                            }
                            camct = 0;

                        }

                        isSh2 = false;
                        camct++;

                    }
                }
            }

            // en3------------------------------------------------
            for (int i = 0; i < En3.Count; i++)
            {
                En3[i].L[En3[i].index].j++;


                if (En3[i].L[En3[i].index].j >= En3[i].L[En3[i].index].img.Count - 1 && En3[i].L[En3[i].index].isExsit != false)
                    En3[i].L[En3[i].index].j = 0;

                if (En3[i].L[1].j >= En3[i].L[1].img.Count - 1)
                    En3[i].L[1].j = En3[i].L[1].img.Count - 1;

            }
            // move hero------------
            en3Check();
            for (int k = 0; k < En3.Count; k++)
            {
                for (int i = 0; i < En3[k].L.Count; i++)
                {

                    if (En3[k].L[En3[k].index].j >= En3[k].L[En3[k].index].img.Count - 1 && En3[k].L[En3[k].index].isExsit != false)
                        En3[k].L[En3[k].index].j = 0;

                }

            }

            // -----------------------boss------------------------ 
           
            if (background[3].RcSrc.X >= 3360)
            {
               
                if (boss[0].RcDst.Y >= 150)
                {
                    bossCheck();
                    if (bossCt <= 30)
                    {
                        for (int i = 0; i < boss.Count; i++)
                        {

                            boss[i].RcDst.X -= 11;
                        }
                        for (int i = 0; i < bossFire.Count; i++)
                        {
                            bossFire[i].RcDst.X -= 11;
                            bossFire2[i].RcDst.X -= 11;
                        }
                    }
                    else
                    {
                        if (bossCt >= 30 && bossCt <= 60)
                        {
                            for (int i = 0; i < boss.Count; i++)
                            {

                                boss[i].RcDst.X += 11;
                            }
                            for (int i = 0; i < bossFire.Count; i++)
                            {
                                bossFire[i].RcDst.X += 11;
                                bossFire2[i].RcDst.X += 11;
                            }
                           
                        }
                        else
                            bossCt = 0;
                    }
                    bossCt++;
                }
                else
                {
                    for (int i = 0; i < boss.Count; i++)
                    {

                        boss[i].RcDst.Y += 6;
                    }
                    for (int i = 0; i < bossFire.Count; i++)
                    {
                        bossFire[i].RcDst.Y += 6;
                        bossFire2[i].RcDst.Y += 6;
                    }
                }
            }
          
            bossFire[0].j++;
            if (bossFire[bossFire[0].index].j >= bossFire[bossFire[0].index].img.Count&& bossFire[0].index==0)
                bossFire[bossFire[0].index].j = 0;

            bossFire2[0].j++;
            if (bossFire2[bossFire2[0].index].j >= bossFire2[bossFire2[0].index].img.Count&& bossFire[0].index == 0)
                bossFire2[bossFire2[0].index].j = 0;
            if (bossHit == 20)
            {

                boss[0].index = 1;

            }
            if (boss[0].index==1)
            {
                bossFire[bossFire2[0].index].j++;
                bossFire2[bossFire2[0].index].j++;
                if (bossFire2[bossFire2[0].index].j >= bossFire2[bossFire2[0].index].img.Count)
                {
                    bossFire2[bossFire2[0].index].j = 0;
                    bossFire[bossFire2[0].index].j = 0;
                    if (bossFire2[0].index < 3)
                    {
                        bossFire2[0].index++;
                        bossFire[0].index++;
                    }
                   

                }
            }

            if (boss[0].index == 0)
            {
                boss[boss[0].index].j++;
                if (boss[boss[0].index].j >= boss[boss[0].index].img.Count)
                    boss[boss[0].index].j = 0;
            }
            if(bossHit>100)
            {
                boss[boss[0].index].j++;
               
                if (boss[boss[0].index].j >= boss[boss[0].index].img.Count)
                    boss[boss[0].index].j = boss[boss[0].index].img.Count-1;
            }

            //if (bossHit == 130)
            //{
            //    boss[boss[0].index].j++;
            //    if (boss[boss[0].index].j >= boss[boss[0].index].img.Count)
            //        boss[boss[0].index].j = 0;
            //}
            //if (bossHit == 140)
            //{
            //    boss[boss[0].index].j++;
            //    if (boss[boss[0].index].j >= boss[boss[0].index].img.Count)
            //        boss[boss[0].index].j = 0;
            //}

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (!menu)
            {
                //run = true;
                cActor pnn = new cActor();
                if (!isUP)
                {
                    if (!isLeft)
                    {
                        charIndex = 8;
                        pnn.RcDst = new Rectangle(Hero[8].RcDst.X + Hero[8].RcDst.Width, Hero[8].RcDst.Y + 68, 25, 20);
                    }
                    else
                    {
                        charIndex = 20;
                        pnn.RcDst = new Rectangle(Hero[20].RcDst.X - Hero[20].RcDst.Width, Hero[20].RcDst.Y + 68, 25, 20);
                    }
                    pnn.up = true;
                }
                else
                {
                   
                    pnn.RcDst = new Rectangle(Hero[8].RcDst.X + (Hero[8].RcDst.Width - 110) / 2, Hero[8].RcDst.Y - 5, 25, 20);
                }

                if(issit)
                {
                    if (!isLeft)
                    {
                        charIndex = 17;
                        pnn.RcDst = new Rectangle(Hero[17].RcDst.X + Hero[17].RcDst.Width, Hero[17].RcDst.Y + 68, 25, 20);
                    }
                    else
                    {
                        charIndex = 18;
                        pnn.RcDst = new Rectangle(Hero[17].RcDst.X - Hero[17].RcDst.Width, Hero[17].RcDst.Y + 68, 25, 20);
                    }
                   
                }
                //------------------------

                Bitmap pv = new Bitmap("bullet.png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);
                pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);

                bullet.Add(pnn);

                isShot = true;



              
            }
            //------------menubar---------------------
            if (select)
            {
                for (int i = 0; i < SelectHero.Count; i++)
                {
                    if (e.X >= SelectHero[i].x
                        && e.X <= SelectHero[i].x + 320
                        && e.Y >= SelectHero[i].y
                        && e.Y <= SelectHero[i].y + 480
                        )
                    {

                        if (i == 2)
                            slidedown = true;


                    }

                }
            }

            for (int i = 0; i < menuSt.Count; i++)
            {
                if (e.X >= menuSt[i].x
                    && e.X <= menuSt[i].x + 300
                    && e.Y >= menuSt[i].y
                    && e.Y <= menuSt[i].y + 100
                    )
                {
                    if (i == 0)
                    {
                        select = true;
                    }
                }

            }
          


            //---------------------------------
        }
        public void check()
        {
            for (int i = 0; i < En.Count; i++)
            {
                if (Hero[charIndex].RcDst.X+70 >= En[i].L[En[i].index].RcDst.X
                            && Hero[charIndex].RcDst.X <= En[i].L[En[i].index].RcDst.Right
                            )
                {
                    if (En[i].L[En[i].index].isExsit)
                        isHit = true;
                    else
                        isHit = false;

                    enmindex1 = i;
                }
               
            }
            for (int k = 0; k < bullet.Count; k++)
            {
                for (int i = 0; i < En.Count; i++)
                {
                    if (bullet[k].RcDst.X+20 >= En[i].L[En[i].index].RcDst.X
                                && bullet[k].RcDst.X <= En[i].L[En[i].index].RcDst.Right
                                && bullet[k].RcDst.Y >= En[i].L[En[i].index].RcDst.Y
                                && bullet[k].RcDst.Y <= En[i].L[En[i].index].RcDst.Bottom
                                )
                    {
                        if (En[i].L[En[i].index].isExsit)
                            isSh = true;
                        else
                            isSh = false;

                        pos = k;
                        enmindex2 = i;


                    }

                }
            }

        }
        public void check2()
        {
            for (int i = 0; i < En2.Count; i++)
            {
                if (Hero[charIndex].RcDst.X + 400 >= En2[i].L[En2[i].index].RcDst.X
                            && Hero[charIndex].RcDst.X <= En2[i].L[En2[i].index].RcDst.Right
                            )
                {
                    if (En2[i].L[En2[i].index].isExsit)
                        isHit2 = true;
                    else
                        isHit2 = false;

                    enmindex3 = i;
                }

            }
            for (int k = 0; k < En2.Count; k++)
            {
                for (int i = 0; i < bullet.Count; i++)
                {
                    if (bullet[i].RcDst.X + 20 >= En2[k].L[En2[k].index].RcDst.X
                                && bullet[i].RcDst.X <= En2[k].L[En2[k].index].RcDst.Right
                                && bullet[i].RcDst.Y >= En2[k].L[En2[k].index].RcDst.Y
                                && bullet[i].RcDst.Y <= En2[k].L[En2[k].index].RcDst.Bottom
                                )
                    {
                        if (En2[k].L[En2[k].index].isExsit)
                            isSh2 = true;
                        else
                            isSh2 = false;

                        pos = i;
                        enmindex4 = k;


                    }

                }
            }

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // to reset the hero
            if (jumpFlag == 0 &&init&&!jum)
            {
                for (int i = 0; i < Hero.Count; i++)
                {
                    Hero[i].j = 0;
                }
                if (isLeft)
                    charIndex = 1;
                else
                    charIndex = 0;

                init = false;
                jumpLeft = false;
            }

            if(isUP)
            {
                Hero[charIndex].j = 0;
                if (!isLeft)
                    charIndex = 0;
                else
                    charIndex = 1;
                Hero[charIndex].j = 0;
                isUP = false;
            }
            run = false;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            //-------------- Move hero left----------------------
            if (e.KeyCode==Keys.D)
            {
                run = true;
                jumpLeft = true;
                isLeft = false;
                if (!isShot)
                {
                    isLeft = false;
                    if (charIndex == 5)
                    {
                        charIndex = 5;
                        Hero[charIndex].j++;
                    }
                    else
                    {
                        charIndex = 2;
                        Hero[charIndex].j++;
                    }

                    if (isUP)
                    {
                        
                            charIndex = 15;
                       
                        Hero[charIndex].j++;
                    }
                }
                else
                {
                    if (isUP)
                        charIndex = 16;
                    else
                    charIndex = 10;
                }

                check2();
                if (Hero[charIndex].RcDst.Right >= this.Width - 800&&!isHit2 && background[3].RcSrc.X < 3400)
                {
                   
                    check();
                   
                    if (!isHit)
                    {
                        flag = 1;
                        for (int i = 0; i < background.Count; i++)
                        {
                            background[i].RcSrc.X += 3;
                        }
                        if (Hero[charIndex].isExsit)
                        {
                            for (int i = 0; i < En.Count; i++)
                            {
                                for (int k = 0; k < En[i].L.Count; k++)
                                {
                                    En[i].L[k].RcDst.X -= 10;
                                }
                            }
                           
                        }
                        for (int i = 0; i < prisoner.Count; i++)
                        {
                            prisoner[i].L[0].RcDst.X -= 8;
                        }

                        for (int i = 0; i < En2.Count; i++)
                        {
                            for (int k = 0; k < En2[i].L.Count; k++)
                            {
                                En2[i].L[k].RcDst.X -= 10;
                            }
                        }
                        for (int i = 0; i < En3.Count; i++)
                        {
                            for (int k = 0; k < En3[i].L.Count; k++)
                            {
                                En3[i].L[k].RcDst.X -= 10;
                            }
                        }
                        for (int i = 0; i < car.Count; i++)
                        {
                            car[i].RcDst.X -= 11;
                        }
                        for (int i = 1; i < L.Count; i++)
                        {
                            L[i].RcDst.X -= 10;
                        }

                    }
                    for (int i = 0; i < boss.Count; i++)
                    {

                        boss[i].RcDst.X -= 11;
                    }
                    for (int i = 0; i < bossFire.Count; i++)
                    {
                        bossFire[i].RcDst.X -= 11;
                        bossFire2[i].RcDst.X -= 11;
                    }
                    check2();
                   
                    
                  
                }
                else
                {
                    check();
                    for (int i = 0; i < Hero.Count; i++)
                    {
                        if (!isHit)
                        {
                           
                            Hero[i].RcDst.X += 11;
                        }
                        
                    }
                }
                if(background[3].RcSrc.X >=170 && background[3].RcSrc.X<172)
                {
                    
                    drawSubBackground();
                   
                }

                if (!jum)
                    init = true;
                // MessageBox.Show("" + background[3].RcSrc.Left);
                isHit = false;
               
            }

            //-------------- Move hero right----------------------
            if (e.KeyCode == Keys.A)
            {
                run = true;
                isLeft = true;
                if (!isUP)
                    charIndex = 3;
                else
                {
                    if (!isShot)
                        charIndex = 24;
                    else
                        charIndex = 23;
                }
               
                for (int i = 0; i < Hero.Count; i++)
                {
                    
                    
                        Hero[i].RcDst = new Rectangle(
                                            Hero[i].RcDst.Left - 15,
                                            Hero[i].RcDst.Top,
                                            Hero[i].RcDst.Width,
                                            Hero[i].RcDst.Height);
                    
                }
                Hero[charIndex].j++;
                init = true;
            }

            //--------------Jump----------------------
            if (e.KeyCode == Keys.Space)
            {
                
                if(jumpLeft)
                {
                    charIndex = 11;
                    jumpLeft = false;
                    jum = true;
                }
                else
                {
                    jumpFlag = 1;
                }
            }

            // -------------attack with knife----------------
            if (e.KeyCode == Keys.F)
            {
                check();
               
                if (isLeft)
                {
                    charIndex = 7;
                }
                else
                {
                    charIndex = 6;
                }
                if (isHit)
                {
                    En[enmindex1].index = 1;
                    for (int k = 0; k < En[enmindex2].L.Count; k++)
                    {
                        En[enmindex1].L[k].isExsit = false;
                    }
                    isHit = false;
                }
                for(int i=0;i<En3.Count;i++)
                {
                    if(En3[i].index==3)
                    {
                        eneCt++;
                        if (eneCt == 5)
                        {
                            for (int k = 0; k < En3[i].L.Count; k++)
                            {
                                En3[i].L[k].isExsit = false;
                                //En3[i].L[k].j = 0;
                            }
                          
                           
                            eneCt = 0;
                        }
                    }
                  
                }
               
               
            }

            if (e.KeyCode == Keys.S)
            {
                if (!isLeft)
                    charIndex = 5;
                else
                    charIndex = 19;

               Hero[charIndex].j++;
                issit = true;

            }

            if (e.KeyCode == Keys.W)
            {
                isUP = true;
                if (!isLeft)
                    charIndex = 13;
                else
                    charIndex = 21;


            }

            if(e.KeyCode==Keys.C)
            {
                change();
            }
            //DrawDubb(this.CreateGraphics());
        }
        //-----------------------draw idle hero------------------------
        public void drawBackground()
        {

            //fou layer
            cActor pnn = new cActor();
            Bitmap pv = new Bitmap("back/4.png");
            pnn.img.Add(pv);
            pnn.RcSrc = new Rectangle(0, 0, 500, 160);
            pnn.RcDst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            background.Add(pnn);

            //third layer
             pnn = new cActor();
             pv = new Bitmap("back/3.png");
            pnn.img.Add(pv);
            pnn.RcSrc = new Rectangle(0, 0, 500, 180);
            pnn.RcDst = new Rectangle(0, 400, this.ClientSize.Width, this.ClientSize.Height);
            background.Add(pnn);

            //sec layer
            pnn = new cActor();
            pv = new Bitmap("back/2.png");
            pv.MakeTransparent(pv.GetPixel(0, 0));
            pnn.img.Add(pv);
            pnn.RcSrc = new Rectangle(0, 0, 500, 300);
            pnn.RcDst = new Rectangle(0, 400, this.ClientSize.Width, this.ClientSize.Height);
            background.Add(pnn);

            //first layer
            pnn = new cActor();
            pv = new Bitmap("back/main.png");
            pv.MakeTransparent(pv.GetPixel(0, 0));
            pnn.img.Add(pv);
            pnn.RcSrc = new Rectangle(0, 0, 500, 230);
            pnn.RcDst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            background.Add(pnn);

           
        }
        public void drawSubBackground()
        {
            
            cActor pnn = new cActor();
            Bitmap pv = new Bitmap("back/frame.png");
            pv.MakeTransparent(pv.GetPixel(0, 0));
            pnn.img.Add(pv);
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);
            pnn.RcDst = new Rectangle(1090+background[3].RcSrc.X, 500, pnn.img[0].Width+200, pnn.img[0].Height+200);
            L.Add(pnn);
        }
        public void drawHeroIDLE()
        {
            // index 0
            cActor pnn = new cActor();
           
            for(int i=1;i<3;i++)
            {
                Bitmap pv = new Bitmap(folder+ "/id/"+i+".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);
               
            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 41);
            pnn.RcDst = new Rectangle(500, 620, 150,150);
            Hero.Add(pnn);

            pnn = new cActor();
            // index 1
            for (int i = 1; i < 3; i++)
            {
                Bitmap pv = new Bitmap(folder+"/id/left/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 41);
            pnn.RcDst = new Rectangle(500, 620, 150, 150);
            Hero.Add(pnn);
        }
        public void drawHeroKnife()
        {
            // hero jump, index 6
            cActor pnn = new cActor();

            for (int i = 1; i <= 6; i++)
            {
                Bitmap pv = new Bitmap(folder+"/knife/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 50, 60);
            pnn.RcDst = new Rectangle(460, 570, 200, 200);
            Hero.Add(pnn);
            // hero jump, index 7
             pnn = new cActor();

            for (int i = 1; i <= 6; i++)
            {
                Bitmap pv = new Bitmap(folder+"/knife/left/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 50, 60);
            pnn.RcDst = new Rectangle(450, 560, 200, 200);
            Hero.Add(pnn);
        }
        public void drawHeroBullet()
        {
            // index 8
            cActor pnn = new cActor();
            for (int i = 1; i <= 6; i++)
            {
                Bitmap pv = new Bitmap(folder+"/bullet/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);
            pnn.RcDst = new Rectangle(500, 590, 200, 180);
            Hero.Add(pnn);
        }
        public void drawHeroLanding()
        {
            // index =9
            cActor pnn = new cActor();

            for (int i = 1; i <= 7; i++)
            {
                Bitmap pv = new Bitmap(folder+"/landing/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 50, 250);
            pnn.RcDst = new Rectangle(460, -1, 200, 250);
            Hero.Add(pnn);

            // attack and run index 10
             pnn = new cActor();
            for (int i = 1; i <= 10; i++)
            {
                Bitmap pv = new Bitmap(folder+"/attRun/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);
            pnn.RcDst = new Rectangle(500, 605, 200, 180);
            Hero.Add(pnn);

            // jump left index 11
            pnn = new cActor();
            for (int i = 1; i <= 6; i++)
            {
                Bitmap pv = new Bitmap(folder+"/leftRun/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 50);
            pnn.RcDst = new Rectangle(500, 600, 180, 180);
            Hero.Add(pnn);

            // death index 12
            pnn = new cActor();
            for (int i = 1; i <= 19; i++)
            {
                Bitmap pv = new Bitmap(folder + "/death/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 50);
            pnn.RcDst = new Rectangle(500, 650, 180, 180);
            Hero.Add(pnn);
            // up index 13
            pnn = new cActor();
            for (int i = 1; i <= 2; i++)
            {
                Bitmap pv = new Bitmap(folder + "/up/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 77);
            pnn.RcDst = new Rectangle(510, 490, 150, 280);
            Hero.Add(pnn);

            // fireUp index 14
            pnn = new cActor();
            for (int i = 1; i <= 9; i++)
            {
                Bitmap pv = new Bitmap(folder + "/fireUp/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 77);
            pnn.RcDst = new Rectangle(510, 490, 150, 280);
            Hero.Add(pnn);
            //runUp index 15
            pnn = new cActor();
            for (int i = 1; i <= 10; i++)
            {
                Bitmap pv = new Bitmap(folder + "/runUp/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 80);
            pnn.RcDst = new Rectangle(510, 490, 150, 280);
            Hero.Add(pnn);
            //runUp with fire index 16
            pnn = new cActor();
            for (int i = 1; i <= 10; i++)
            {
                Bitmap pv = new Bitmap(folder + "/fireUpRun/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 80);
            pnn.RcDst = new Rectangle(510, 490, 150, 280);
            Hero.Add(pnn);
            ////sitFire with fire index 17
            pnn = new cActor();
            for (int i = 1; i <= 4; i++)
            {
                Bitmap pv = new Bitmap(folder + "/sitFire/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 55, 80);
            pnn.RcDst = new Rectangle(510, 700, 150, 280);
            Hero.Add(pnn);
        }
        public void darwHeroRight()
        {
            ////sitFire with fire index 18
            cActor pnn = new cActor();
            for (int i = 1; i <= 4; i++)
            {
                Bitmap pv = new Bitmap(folder + "/sitFire/right/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 55, 80);
            pnn.RcDst = new Rectangle(510, 700, 150, 280);
            Hero.Add(pnn);

            // Hero sit right index 19
            pnn = new cActor();
            for (int i = 1; i <= 4; i++)
            {
                Bitmap pv = new Bitmap(folder + "/sit/right/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 50, 60);
            pnn.RcDst = new Rectangle(470, 660, 200, 200);
            Hero.Add(pnn);
            // Hero fire right index 20
            pnn = new cActor();
            for (int i = 1; i <= 6; i++)
            {
                Bitmap pv = new Bitmap(folder + "/bullet/right/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);
            pnn.RcDst = new Rectangle(490, 590, 200, 180);
            Hero.Add(pnn);

            // Hero up index 21
            pnn = new cActor();
            for (int i = 1; i <= 2; i++)
            {
                Bitmap pv = new Bitmap(folder + "/up/right/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 77);
            pnn.RcDst = new Rectangle(510, 490, 150, 280);
            Hero.Add(pnn);
            // Hero up index 22
            pnn = new cActor();
            for (int i = 1; i <= 11; i++)
            {
                Bitmap pv = new Bitmap(folder + "/fireUp/right/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 77);
            pnn.RcDst = new Rectangle(510, 490, 150, 280);
            Hero.Add(pnn);

            // Hero up index 23
            pnn = new cActor();
            for (int i = 1; i <= 10; i++)
            {
                Bitmap pv = new Bitmap(folder + "/fireUpRun/right/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 77);
            pnn.RcDst = new Rectangle(510, 490, 150, 280);
            Hero.Add(pnn);

            // Hero up index 24
            pnn = new cActor();
            for (int i = 1; i <= 10; i++)
            {
                Bitmap pv = new Bitmap(folder + "/runUp/right/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 38, 77);
            pnn.RcDst = new Rectangle(510, 490, 150, 280);
            Hero.Add(pnn);
        }
        public void drawFirstEn()
        {
            int r1 = rr.Next(10, 20);
            for (int k = 0; k < r1; k++)
            {

                int r2 = rr.Next(2000, 20000);
                cActor2 pnn2 = new cActor2();
                cActor pnn = new cActor();
                for (int i = 1; i <= 6; i++)
                {
                    Bitmap pv = new Bitmap("En/first/id/" + i + ".png");
                    pv.MakeTransparent(pv.GetPixel(0, 0));
                    pnn.img.Add(pv);

                }
                pnn.RcSrc = new Rectangle(0, 0, 50, 60);
                pnn.RcDst = new Rectangle(r2, 600, 200, 200);
                pnn2.L.Add(pnn);

                // die

                pnn = new cActor();
                for (int i = 1; i <= 14; i++)
                {
                    Bitmap pv = new Bitmap("En/first/die/" + i + ".png");
                    pv.MakeTransparent(pv.GetPixel(0, 0));
                    pnn.img.Add(pv);

                }
                pnn.RcSrc = new Rectangle(0, 0, 70, 60);
                pnn.RcDst = new Rectangle(r2, 570, 250, 200);
                pnn2.L.Add(pnn);

                pnn = new cActor();
                for (int i = 1; i <= 13; i++)
                {
                    Bitmap pv = new Bitmap("En/first/run/" + i + ".png");
                    pv.MakeTransparent(pv.GetPixel(0, 0));
                    pnn.img.Add(pv);

                }
                pnn.RcSrc = new Rectangle(0, 0, 70, 60);
                pnn.RcDst = new Rectangle(r2, 600, 250, 200);
                pnn2.L.Add(pnn);
               
                pnn = new cActor();
                for (int i = 1; i <= 8; i++)
                {
                    Bitmap pv = new Bitmap("En/first/attack/" + i + ".png");
                    pv.MakeTransparent(pv.GetPixel(0, 0));
                    pnn.img.Add(pv);

                }
                pnn.RcSrc = new Rectangle(0, 0, 70, 60);
                pnn.RcDst = new Rectangle(r2-30, 600, 250, 200);
                pnn2.L.Add(pnn);
                En.Add(pnn2);

                En[k].index = 2;
                //----------------------------------
            }

        }
        public void drawSecEn()
        {

            cActor2 pnn2 = new cActor2();
            cActor pnn = new cActor();
            for (int i = 1; i <= 6; i++)
            {
                Bitmap pv = new Bitmap("En/sec/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 90, 90);
            pnn.RcDst = new Rectangle(8000, 450, 400, 400);
            pnn2.L.Add(pnn);
           
            // sit index 1
            
             pnn = new cActor();
            for (int i = 1; i <= 3; i++)
            {
                Bitmap pv = new Bitmap("En/sec/sit/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 74, 88);
            pnn.RcDst = new Rectangle(8000, 500, 330, 400);
            pnn2.L.Add(pnn);
           
            //fire
            pnn = new cActor();
            for (int i = 1; i <= 10; i++)
            {
                Bitmap pv = new Bitmap("En/sec/fire/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 74, 88);
            pnn.RcDst = new Rectangle(8000, 500, 330, 400);
            pnn2.L.Add(pnn);
            // death index 3
            pnn = new cActor();
            for (int i = 1; i <= 10; i++)
            {
                Bitmap pv = new Bitmap("En/sec/death/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 80, 88);
            pnn.RcDst = new Rectangle(8000, 530, 350, 400);
            pnn2.L.Add(pnn);


            En2.Add(pnn2);
        }
        public void drawBoss()
        {
            cActor pnn = new cActor();

            for (int i = 1; i <= 4; i++)
            {
                Bitmap pv = new Bitmap("boss/id/" + i + ".png");
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);
            pnn.RcDst = new Rectangle(13000, -500, pnn.img[0].Width+700, pnn.img[0].Height+260);
            boss.Add(pnn);

            pnn = new cActor();
            for (int i = 1; i <= 5; i++)
            {
                Bitmap pv = new Bitmap("boss/dest/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);
            pnn.RcDst = new Rectangle(13000, -500, pnn.img[0].Width + 700, pnn.img[0].Height + 260);
            boss.Add(pnn);


            pnn = new cActor();
            for (int i = 1; i <=1; i++)
            {
                Bitmap pv = new Bitmap("boss/hit/" + i + ".png");
               
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);
            pnn.RcDst = new Rectangle(13000, -500, pnn.img[0].Width + 700, pnn.img[0].Height + 260);
            boss.Add(pnn);

        }
        public void drawbossFire()
        {
            cActor pnn = new cActor();
            for (int i = 1; i <= 4; i++)
            {
                Bitmap pv = new Bitmap("boss/fire/id/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);
            pnn.RcDst = new Rectangle(boss[0].RcDst.X + 110, boss[0].RcDst.Y+300, pnn.img[0].Width+100, pnn.img[0].Height+100);
            bossFire.Add(pnn);

            pnn = new cActor();
            for (int i = 1; i <= 9; i++)
            {
                Bitmap pv = new Bitmap("boss/fire/attack/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[8].Width, pnn.img[8].Height);
            pnn.RcDst = new Rectangle(boss[0].RcDst.X + 110, boss[0].RcDst.Y + 300, pnn.img[8].Width + 100, pnn.img[8].Height + 100);
            bossFire.Add(pnn);

            pnn = new cActor();
            for (int i = 1; i <= 16; i++)
            {
                Bitmap pv = new Bitmap("boss/fire/attack2/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[8].Width, pnn.img[8].Height);
            pnn.RcDst = new Rectangle(boss[0].RcDst.X + 110, boss[0].RcDst.Y + 300, pnn.img[8].Width + 100, pnn.img[8].Height + 100);
            bossFire.Add(pnn);

            pnn = new cActor();
            for (int i = 1; i <= 6; i++)
            {
                Bitmap pv = new Bitmap("boss/fire/attack3/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[5].Width, pnn.img[5].Height);
            pnn.RcDst = new Rectangle(boss[0].RcDst.X + 110, boss[0].RcDst.Y + 300, pnn.img[5].Width + 100, pnn.img[5].Height + 100);
            bossFire.Add(pnn);

            //2
            pnn = new cActor();
            for (int i = 1; i <= 4; i++)
            {
                Bitmap pv = new Bitmap("boss/fire/id/right/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);
            pnn.RcDst = new Rectangle(boss[1].RcDst.X + 740, boss[0].RcDst.Y + 300, pnn.img[0].Width + 100, pnn.img[0].Height + 100);
            bossFire2.Add(pnn);

            pnn = new cActor();
            for (int i = 1; i <= 9; i++)
            {
                Bitmap pv = new Bitmap("boss/fire/attack/right/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[8].Width, pnn.img[8].Height);
            pnn.RcDst = new Rectangle(boss[0].RcDst.X + 740, boss[0].RcDst.Y + 300, pnn.img[8].Width + 100, pnn.img[8].Height + 100);
            bossFire2.Add(pnn);

            pnn = new cActor();
            for (int i = 1; i <= 9; i++)
            {
                Bitmap pv = new Bitmap("boss/fire/attack2/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[8].Width, pnn.img[8].Height);
            pnn.RcDst = new Rectangle(boss[0].RcDst.X + 740, boss[0].RcDst.Y + 300, pnn.img[8].Width + 100, pnn.img[8].Height + 100);
            bossFire2.Add(pnn);

            pnn = new cActor();
            for (int i = 1; i <= 6; i++)
            {
                Bitmap pv = new Bitmap("boss/fire/attack3/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[5].Width, pnn.img[5].Height);
            pnn.RcDst = new Rectangle(boss[0].RcDst.X + 740, boss[0].RcDst.Y + 300, pnn.img[5].Width + 100, pnn.img[5].Height + 100);
            bossFire2.Add(pnn);

        }
        public void drawThirdEn()
        {
            int r1 = rr.Next(5, 10);
            for (int k = 0; k < r1; k++)
            {

                int r2 = rr.Next(10000, 60000);
                cActor2 pnn2 = new cActor2();
                cActor pnn = new cActor();
                for (int i = 1; i <= 6; i++)
                {
                    Bitmap pv = new Bitmap("En/third/id/" + i + ".png");
                    pv.MakeTransparent(pv.GetPixel(0, 0));
                    pnn.img.Add(pv);

                }
                pnn.RcSrc = new Rectangle(0, 0, 50, 60);
                pnn.RcDst = new Rectangle(r2, 630, 200, 200);
                pnn2.L.Add(pnn);

                // die
                pnn = new cActor();
                for (int i = 1; i <= 6; i++)
                {
                    Bitmap pv = new Bitmap("En/third/die/" + i + ".png");
                    pv.MakeTransparent(pv.GetPixel(0, 0));
                    pnn.img.Add(pv);

                }
                pnn.RcSrc = new Rectangle(0, 0, 70, 60);
                pnn.RcDst = new Rectangle(r2, 630, 250, 200);
                pnn2.L.Add(pnn);

                pnn = new cActor();
                for (int i = 1; i <= 12; i++)
                {
                    Bitmap pv = new Bitmap("En/third/run/" + i + ".png");
                    pv.MakeTransparent(pv.GetPixel(0, 0));
                    pnn.img.Add(pv);

                }
                pnn.RcSrc = new Rectangle(0, 0, 70, 60);
                pnn.RcDst = new Rectangle(r2, 630, 250, 200);
                pnn2.L.Add(pnn);

                pnn = new cActor();
                for (int i = 1; i <= 8; i++)
                {
                    Bitmap pv = new Bitmap("En/third/attack/" + i + ".png");
                    pv.MakeTransparent(pv.GetPixel(0, 0));
                    pnn.img.Add(pv);

                }
                pnn.RcSrc = new Rectangle(0, 0, 70, 60);
                pnn.RcDst = new Rectangle(r2 - 30, 600, 250, 200);
                pnn2.L.Add(pnn);
                En3.Add(pnn2);

                En3[k].index = 2;
                //----------------------------------
            }
        }
        public void drawCar()
        {
            cActor pnn = new cActor();
            for (int i = 1; i <= 4; i++)
            {
                Bitmap pv = new Bitmap("En/car/"+i+".png");
               
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, pnn.img[0].Width, pnn.img[0].Height);
            pnn.RcDst = new Rectangle(6000, 460, 550, 300);
            car.Add(pnn);
        }
        public void drawPrisoner()
        {
            int r1 = rr.Next(10, 20);
            for (int k = 0; k <1; k++)
            {

                int r2 = rr.Next(2000, 20000);
                cActor2 pnn2 = new cActor2();
                cActor pnn = new cActor();
                for (int i = 1; i <= 10; i++)
                {
                    Bitmap pv = new Bitmap("pr/id/" + i + ".png");
                    pv.MakeTransparent(pv.GetPixel(0, 0));
                    pnn.img.Add(pv);

                }
                pnn.RcSrc = new Rectangle(0, 0, 50, 60);
                pnn.RcDst = new Rectangle(1960, 530, 200, 200);
                pnn2.L.Add(pnn);

                prisoner.Add(pnn2);

                prisoner[k].index = 0;
                //----------------------------------
            }
        }
        public void drawHeroMove()
        {
            // right side, index 2
            cActor pnn = new cActor();
            for (int i = 1; i <= 11; i++)
            {
                Bitmap pv = new Bitmap(folder + "/run/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 10, 38, 41);
            pnn.RcDst = new Rectangle(500, 620, 150, 150);
            Hero.Add(pnn);

            // left side, index 3
            pnn = new cActor();
            for (int i = 1; i <= 11; i++)
            {
                Bitmap pv = new Bitmap(folder + "/run/left/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 10, 38, 41);
            pnn.RcDst = new Rectangle(500, 620, 150, 150);
            Hero.Add(pnn);

            // hero jump, index 4
            pnn = new cActor();
            for (int i = 1; i <= 6; i++)
            {
                Bitmap pv = new Bitmap(folder + "/jump/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 10, 38, 50);
            pnn.RcDst = new Rectangle(500, 620, 150, 150);
            Hero.Add(pnn);

            //hero sit, index 5
            pnn = new cActor();
            for (int i = 1; i <= 4; i++)
            {
                Bitmap pv = new Bitmap(folder + "/sit/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);

            }
            pnn.RcSrc = new Rectangle(0, 0, 50, 60);
            pnn.RcDst = new Rectangle(470, 660, 200, 200);
            Hero.Add(pnn);

        }
        public void menubar()
        {
            int y = 250;
            for (int i = 1; i <= 3; i++)
            {
                cActor pnn = new cActor();
                Bitmap pv = new Bitmap("menu/"+i+".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);
                pv = new Bitmap("menu/hover/" + i + ".png");
                pv.MakeTransparent(pv.GetPixel(0, 0));
                pnn.img.Add(pv);
                pnn.x = 580;
                pnn.y = y;
                pnn.j = 0;
                y += 130;
                menuSt.Add(pnn);
            }

           
        }
        public void selectHero()
        {
            int x = 85;
            for (int i = 1; i <= 4; i++)
            {
                cActor pnn = new cActor();
                Bitmap pv = new Bitmap("menu/select/hero/" + i + ".png");
               
                pnn.img.Add(pv);
                pv = new Bitmap("menu/select/hero/hover/" + i + ".png");
               
                pnn.img.Add(pv);
                pnn.x = x;
                pnn.y = 280;
                pnn.j = 0;
                x += 320;
                SelectHero.Add(pnn);
            }
        }
        public void change()
        {
            // index 1
            int c = 1;
            folder = "hero2";
            for (int i = 0; i < Hero[0].img.Count; i++)
            {
                 Hero[0].img[i] = new Bitmap(folder + "/id/" + c + ".png");
                 Hero[0].img[i].MakeTransparent(Hero[0].img[i].GetPixel(0, 0));
                 c++;
            }
            Hero[0].RcSrc.Width = 40;
            Hero[0].RcSrc.Height = 45;
            Hero[0].RcDst.Height = 160;
            Hero[0].RcDst.Width = 160;
            Hero[0].RcDst.Y = Hero[0].RcDst.Y-5;
            //--------------------------------------
            c = 1;
            for (int i = 0; i < Hero[1].img.Count; i++)
            {
                Hero[1].img[i] = new Bitmap(folder + "/id/left/" + c + ".png");
                Hero[1].img[i].MakeTransparent(Hero[1].img[i].GetPixel(0, 0));
                c++;
            }
            Hero[1].RcSrc.Width = 40;
            Hero[1].RcSrc.Height = 45;
            Hero[1].RcDst.Height = 160;
            Hero[1].RcDst.Width = 160;
            //-------------------------------------
            c = 1;
            for (int i = 0; i < Hero[2].img.Count; i++)
            {
                Hero[2].img[i] = new Bitmap(folder + "/run/" + c + ".png");
                Hero[2].img[i].MakeTransparent(Hero[2].img[i].GetPixel(0, 0));
                c++;
            }
            Hero[2].RcSrc.Y = 0;
            Hero[2].RcSrc.Width = 46;
            Hero[2].RcSrc.Height = 50;
            Hero[2].RcDst.Height = 175;
            Hero[2].RcDst.Width = 175;
            //-------------------------------
            c = 1;
            for (int i = 0; i < Hero[3].img.Count; i++)
            {
                Hero[3].img[i] = new Bitmap(folder + "/run/left/" + c + ".png");
                Hero[3].img[i].MakeTransparent(Hero[3].img[i].GetPixel(0, 0));
                c++;
            }
            Hero[3].RcSrc.Y = 0;
            Hero[3].RcSrc.Width = 46;
            Hero[3].RcSrc.Height = 50;
            Hero[3].RcDst.Height = 175;
            Hero[3].RcDst.Width = 175;
            //-----------------------------
            c = 1;
            for (int i = 1; i < Hero[4].img.Count; i++)
            {
                Hero[4].img[i] = new Bitmap(folder + "/jump/" + c + ".png");
                Hero[4].img[i].MakeTransparent(Hero[4].img[i].GetPixel(0, 0));
                c++;
            }
            c = 1;
            for (int i = 0; i < Hero[5].img.Count; i++)
            {
                Hero[5].img[i] = new Bitmap(folder + "/sit/" + c + ".png");
                Hero[5].img[i].MakeTransparent(Hero[5].img[i].GetPixel(0, 0));
                c++;
            }
            c = 1;
            for (int i = 1; i < Hero[6].img.Count; i++)
            {
                Hero[6].img[i] = new Bitmap(folder + "/knife/" + c + ".png");
                Hero[6].img[i].MakeTransparent(Hero[6].img[i].GetPixel(0, 0));
                c++;
            }
            c = 1;
            for (int i = 1; i < Hero[7].img.Count; i++)
            {
                Hero[7].img[i] = new Bitmap(folder + "/knife/left/" + c + ".png");
                Hero[7].img[i].MakeTransparent(Hero[7].img[i].GetPixel(0, 0));
                c++;
            }
            c = 1;
            for (int i = 0; i < 4; i++)
            {
                Hero[8].img[i] = new Bitmap(folder + "/bullet/" + c + ".png");
                Hero[8].img[i].MakeTransparent(Hero[8].img[i].GetPixel(0, 0));
                c++;
            }
            for (int i = Hero[8].img.Count-1; i >=4; i--)
            {
                Hero[8].img.RemoveAt(i);
            }
            Hero[8].RcSrc.Width = 65;
            Hero[8].RcSrc.Height = 50;
            Hero[8].RcDst.Height = 180;
            Hero[8].RcDst.Width = 230;
            Hero[8].RcDst.Y = Hero[0].RcDst.Y - 5;
            //-----------------------------------------

            //c = 0;
            //for (int i = 1; i < 10; i++)
            //{
            //    Hero[9].img[c] = new Bitmap(folder + "/attRun/" + i + ".png");
            //    Hero[9].img[c].MakeTransparent(Hero[0].img[c].GetPixel(0, 0));
            //    c++;
            //}
            //c = 0;
            //for (int i = 1; i < 6; i++)
            //{
            //    Hero[10].img[c] = new Bitmap(folder + "/leftRun/" + i + ".png");
            //    Hero[10].img[c].MakeTransparent(Hero[0].img[c].GetPixel(0, 0));
            //    c++;
            //}
        }
        private void T_Tick(object sender, EventArgs e)
        {
            // idle hero 
            if (ct % 5 == 0)
            {
                if (isLeft)
                {
                    Hero[1].j++;
                }
                else
                {
                    Hero[0].j++;
                }
                for (int i = 0; i < En.Count; i++)
                {
                    En[i].L[0].j++;
                }
            }

            if (isUP)
            {
              
                if (Hero[charIndex].j < Hero[charIndex].img.Count - 1&&!run)
                    Hero[charIndex].j++;
                
            }
            //-------------------enm---------------------
            if (Hero[charIndex].deathCt>=200)
            {
                charIndex = 12;
                if (Hero[charIndex].j >= Hero[charIndex].img.Count)
                    Hero[charIndex].j = 0;

                heroDeath = true;
                
            }
            if(heroDeath)
            {
               
                Hero[charIndex].j++;
                if (Hero[charIndex].j >= Hero[charIndex].img.Count - 1)
                {
                    Hero[charIndex].j = Hero[charIndex].img.Count - 1;
                    for (int i = 0; i < Hero.Count; i++)
                    {
                        Hero[i].isExsit = false;
                    }
                }
            }
            if (Hero[charIndex].j >= Hero[charIndex].img.Count && !jum && Hero[charIndex].isExsit)
                Hero[charIndex].j = 0;
            // jump 
            if (jumpFlag==1)
            {
                charIndex = 4;
                if (Hero[4].j >= Hero[4].img.Count - 1)
                {
                    end = false;
                    Hero[4].j = Hero[4].img.Count - 1;

                    for (int i = 0; i < Hero.Count; i++)
                    {
                        if (Hero[i].RcDst.Y <= down)
                        {
                           
                        }
                        else
                        {
                            for (int k = 0; k < Hero.Count; k++)
                            {

                                Hero[k].j = 0;
                            }
                            if (isLeft)
                                charIndex = 1;
                            else
                                charIndex = 0;

                            jumpFlag = 0;
                            down = 590;

                        }
                    }
                }
                else
                {
                    end = true;
                    for (int i = 0; i < Hero.Count; i++)
                    {
                        Hero[i].RcDst.Y -= 20;
                       
                    }

                    Hero[4].j++;
                }




            }

            //left jump
            if(jum)
            {
               
                if (Hero[11].j >= Hero[11].img.Count - 1)
                {
                    end = false;
                    Hero[11].j = Hero[11].img.Count - 1;
                    
                    for (int i = 0; i < Hero.Count; i++)
                    {
                        if (Hero[0].RcDst.Y <= down)
                        {
                            Hero[i].RcDst.X += 30;
                        }
                        else
                        {
                            for (int k = 0; k < Hero.Count; k++)
                            {

                                Hero[k].j = 0;
                            }
                            if (isLeft)
                                charIndex = 1;
                            else
                                charIndex = 0;

                            jum = false;
                            down = 590;
                           
                        }
                    }
                }
                else
                {
                    end = true;
                    for (int i = 0; i < Hero.Count; i++)
                    {
                        Hero[i].RcDst.Y -= 20;
                        Hero[i].RcDst.X += 35;
                    }
                   
                        Hero[11].j++;
                }
                
            }

            //------------------ grav-------------------------
            for (int i = 0; i < Hero.Count; i++)
            {


                for (int k = 0; k < L.Count; k++)
                {
                    if (Hero[0].RcDst.X + 130 >= L[k].RcDst.X
                        && Hero[0].RcDst.X <= L[k].RcDst.Right
                        && Hero[0].RcDst.Y <= L[k].RcDst.Y + 120)
                    {
                        down = L[k].RcDst.Y+60 ;

                    }
                    else
                    {
                        down = 590;
                    }

                }
            }

         
            if (Hero[0].RcDst.Y<= down)
                {
                    if (!end)
                    {
                        for (int i = 0; i < Hero.Count; i++)
                        {
                            Hero[i].RcDst.Y += 30;
                        }
                    }
                   
                }
            else
                {
                   
                    down = 590;
                }
            
            //----------------------grav----------------------
            // use knife
            if (charIndex==6)
            {
                if(Hero[charIndex].j<Hero[charIndex].img.Count-1)
                {
                    Hero[charIndex].j++;
                }
                else
                {
                    for (int i = 0; i < Hero.Count; i++)
                    {
                        Hero[i].j = 0;
                    }
                    charIndex = 0;
                }
            }
            if (charIndex == 7)
            {
                if (Hero[charIndex].j < Hero[charIndex].img.Count - 1)
                {
                    Hero[charIndex].j++;
                }
                else
                {
                    for (int i = 0; i < Hero.Count; i++)
                    {
                        Hero[i].j = 0;
                    }
                    charIndex = 1;
                }
            }
            // BULLET
            for(int i=0;i<bullet.Count;i++)
            {
               
                if (bullet[i].up)
                {
                    if (!isLeft)
                    {
                        bullet[i].RcDst = new Rectangle(
                                                        bullet[i].RcDst.Left + 90,
                                                        bullet[i].RcDst.Top,
                                                        bullet[i].RcDst.Width,
                                                        bullet[i].RcDst.Height);
                    }
                    else
                    {
                        bullet[i].RcDst = new Rectangle(
                                                       bullet[i].RcDst.Left - 90,
                                                       bullet[i].RcDst.Top,
                                                       bullet[i].RcDst.Width,
                                                       bullet[i].RcDst.Height);
                    }
                }
                else
                {
                    bullet[i].RcDst = new Rectangle(
                                                   bullet[i].RcDst.Left ,
                                                   bullet[i].RcDst.Top-90,
                                                   bullet[i].RcDst.Width,
                                                   bullet[i].RcDst.Height);
                }

                for (int k = 0; k < car.Count; k++)
                {
                    if (bullet[i].RcDst.X + 20 >= car[k].RcDst.X
                        && bullet[i].RcDst.X <= car[k].RcDst.Left)
                    {
                       
                        cActor pnn = new cActor();
                        Bitmap pv = new Bitmap("En/car/des/1.png");
                        pv.MakeTransparent(pv.GetPixel(0, 0));
                        pnn.img.Add(pv);
                        pnn.RcSrc = car[k].RcSrc;
                        pnn.RcDst = car[k].RcDst;

                        L.Add(pnn);
                        car.RemoveAt(0);
                    }
                }
                check();
                check2();
                bullet[i].bullct++;
                if(bullet[i].bullct>=5)
                {
                    
                  
                    bullet.RemoveAt(i);
                }
              
                if (isSh)
                {
                    try
                    {
                        bullet.RemoveAt(pos);
                    }
                    catch(Exception ex)
                    {

                    }
                    
                    En[enmindex2].index = 1;
                    for (int k = 0; k < En[enmindex2].L.Count; k++)
                    {
                        En[enmindex2].L[k].isExsit = false;
                    }
                    
                    isSh = false;
                   
                }

               
               
            }

            // gun movement
            if (isShot)
            {

                if (!isUP)
                {
                    if (Hero[charIndex].j < Hero[charIndex].img.Count - 1)
                    {
                        Hero[charIndex].j++;
                    }
                    else
                    {


                        for (int i = 0; i < Hero.Count; i++)
                        {
                            Hero[i].j = 0;
                        }

                        if (isLeft)
                            charIndex = 1;
                        else
                            charIndex = 0;

                        isShot = false;
                        run = false;

                        if (issit)
                        {
                            if (!isLeft)
                                charIndex = 5;
                            else
                                charIndex = 19;
                            issit = false;
                        }

                    }
                   
                }
                else
                {
                    if (!run)
                    {
                        if (!isLeft)
                            charIndex = 14;
                        else
                            charIndex = 22;
                        if (Hero[charIndex].j < Hero[charIndex].img.Count - 1)
                        {
                            Hero[charIndex].j++;
                        }
                        else
                        {
                            Hero[charIndex].j = 0;
                            if (!isLeft)
                                charIndex = 13;
                            else
                                charIndex = 21;
                            Hero[charIndex].j = 1;
                            isUP = false;
                            isShot = false;
                            run = false;

                        }
                       
                    }
                    else
                    {
                        if (!isLeft)
                            charIndex = 16;
                        else
                            charIndex = 24;
                        if (Hero[charIndex].j < Hero[charIndex].img.Count - 1)
                        {
                            Hero[charIndex].j++;
                        }
                        else
                        {
                            Hero[charIndex].j = 0;
                            charIndex = 15;
                            isShot = false;
                        }
                       
                    }
                   
                }
            }

            // landing
            if (charIndex==9)
            {
                Hero[charIndex].RcDst.Y += 30;
                if (ct % 4 == 0)
                    Hero[charIndex].j++;
                if (Hero[charIndex].RcDst.Y>=420)
                {
                   
                    

                    for (int i = 0; i < Hero.Count; i++)
                    {
                        Hero[i].j = 0;
                    }
                    if (isLeft)
                        charIndex = 1;
                    else
                        charIndex = 0;

                }
                if (Hero[charIndex].j >= Hero[charIndex].img.Count - 1)
                    Hero[charIndex].j = Hero[charIndex].img.Count - 1;
            }
 
            // ------------------------pr-------------------------
            for(int i=0;i<prisoner.Count;i++)
            {
                if (prisoner[i].L[prisoner[i].index].j >= prisoner[i].L[prisoner[i].index].img.Count - 1)
                    prisoner[i].L[prisoner[i].index].j = 0;
                prisoner[i].L[prisoner[i].index].j++;
            }
            ct++;
            DrawDubb(this.CreateGraphics());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
          
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            menubar();
            selectHero();
            drawHeroIDLE();
            drawHeroMove();
            drawBackground();
            drawHeroKnife();
            drawHeroBullet();
            drawHeroLanding();
            darwHeroRight();
            drawFirstEn();
            drawPrisoner();
            drawSecEn();
            drawThirdEn();
            drawCar();
            drawBoss();
            drawbossFire();
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.SaddleBrown);
            if (!menu)
            {
                for (int i = 0; i < background.Count; i++)
                {

                    g.DrawImage(background[i].img[0], background[i].RcDst, background[i].RcSrc, GraphicsUnit.Pixel);
                }
                for (int i = 0; i < car.Count; i++)
                {
                    g.DrawImage(car[0].img[car[0].j], car[0].RcDst, car[0].RcSrc, GraphicsUnit.Pixel);
                }
                for (int i = 0; i < prisoner.Count; i++)
                {

                    g.DrawImage(prisoner[i].L[prisoner[i].index].img[prisoner[i].L[prisoner[i].index].j], prisoner[i].L[prisoner[i].index].RcDst, prisoner[i].L[prisoner[i].index].RcSrc, GraphicsUnit.Pixel);
                }
                for (int i = 0; i < En.Count; i++)
                {

                    g.DrawImage(En[i].L[En[i].index].img[En[i].L[En[i].index].j], En[i].L[En[i].index].RcDst, En[i].L[En[i].index].RcSrc, GraphicsUnit.Pixel);
                }
                for (int i = 0; i < En3.Count; i++)
                {

                    g.DrawImage(En3[i].L[En3[i].index].img[En3[i].L[En3[i].index].j], En3[i].L[En3[i].index].RcDst, En3[i].L[En3[i].index].RcSrc, GraphicsUnit.Pixel);
                }
                for (int i = 0; i < bullet.Count; i++)
                {
                    g.DrawImage(bullet[i].img[0], bullet[i].RcDst, bullet[i].RcSrc, GraphicsUnit.Pixel);
                }
                g.DrawImage(boss[boss[0].index].img[boss[boss[0].index].j], boss[boss[0].index].RcDst, boss[boss[0].index].RcSrc, GraphicsUnit.Pixel);
                g.DrawImage(bossFire[bossFire[0].index].img[bossFire[bossFire[0].index].j], bossFire[bossFire[0].index].RcDst, bossFire[bossFire[0].index].RcSrc, GraphicsUnit.Pixel);
                g.DrawImage(bossFire2[bossFire2[0].index].img[bossFire2[bossFire2[0].index].j], bossFire2[bossFire2[0].index].RcDst, bossFire2[bossFire2[0].index].RcSrc, GraphicsUnit.Pixel);
                g.DrawImage(Hero[charIndex].img[Hero[charIndex].j], Hero[charIndex].RcDst, Hero[charIndex].RcSrc, GraphicsUnit.Pixel);
              
                for (int i = 0; i < En2.Count; i++)
                {
                    try
                    {
                        g.DrawImage(En2[i].L[En2[i].index].img[En2[i].L[En2[i].index].j], En2[i].L[En2[i].index].RcDst, En2[i].L[En2[i].index].RcSrc, GraphicsUnit.Pixel);
                    }
                    catch
                    {

                    }
                    for (int k = 0; k < En2[i].bullet.Count; k++)
                    {

                        g.DrawImage(En2[i].bullet[k].img[0], En2[i].bullet[k].RcDst, En2[i].bullet[k].RcSrc, GraphicsUnit.Pixel);
                    }
                }
             
                for (int i = 0; i < L.Count; i++)
                {

                    if (flag == 1)
                    {
                        L[i].RcDst.X -= 11;
                        flag = 0;
                    }
                    g.DrawImage(L[i].img[0], L[i].RcDst, L[i].RcSrc, GraphicsUnit.Pixel);
                }
            }
            else
            {
                if (!select)
                {
                    for (int i = 0; i < menuSt.Count; i++)
                    {
                        g.DrawImage(menuSt[i].img[menuSt[i].j], menuSt[i].x, menuSt[i].y, 300, 100);
                    }
                }
                else
                {
                    for (int i = 0; i < SelectHero.Count; i++)
                    {
                        g.DrawImage(SelectHero[i].img[SelectHero[i].j], SelectHero[i].x, SelectHero[i].y, 320, 480);
                    }
                    Bitmap slide = new Bitmap("menu/select/slide.png");
                    g.DrawImage(slide, SelectHero[2].x, dy, 320,480);
                    Bitmap back = new Bitmap("menu/select/back.png");
                    g.DrawImage(back, 0, 0,this.Width,this.Height);

                }
            }



            if(Hero[charIndex].isExsit==false)
            {
                Bitmap over = new Bitmap("gameover.png");
                Rectangle de=new Rectangle(0,0,this.Width,this.Height);
                Rectangle so = new Rectangle(0, 0, over.Width, over.Height);
                g.DrawImage(over, de, so, GraphicsUnit.Pixel);
            }
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
