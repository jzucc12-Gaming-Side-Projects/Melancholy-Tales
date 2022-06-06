using UnityEngine;

namespace CFR.INPUT
{
    public class ManifestNavigator
    {
        PlayerInputManager im;

        #region//Sizing Variables
        bool forShip;
        int myHoldSize = 0;
        int maxHold = 0;
        int shipHoldSize = 0;
        int rightMostPosition = 0;
        #endregion

        #region //Selection Variables
        public int arrow { get; protected set; }
        public int activeItem { get; protected set; }
        public bool activeItemSet => activeItem != -99;
        #endregion

        #region //Position variables
        int[][] spaces = new int[3][];
        int xPos = 0;
        int yPos = 0;
        #endregion

        #region //Frame buffer
        int maxFrameBuffer = 3;
        int currentBuffer = 0;
        #endregion


        #region//Constructor
        public ManifestNavigator(PlayerInputManager _im, bool _forShip, int _maxSize, int _shipSize = 0)
        {
            im = _im;
            forShip = _forShip;
            maxHold = (_forShip ? Globals.maxShipHold : Globals.maxLZHold);
            if(forShip)
            {
                spaces = new int[1][];
                spaces[0] = new int[maxHold];
                for(int ii = 0; ii < maxHold; ii++)
                    spaces[0][ii] = ii;
            }
            shipHoldSize = _shipSize;
            myHoldSize = _maxSize;
            StartUp();
        }
        #endregion

        #region//Start up and Shutdown
        public void StartUp()
        {
            ResetBuffer();
            arrow = 0;
            xPos = 0;
            yPos = 0;
            activeItem = -99;
        }

        public void Shutdown()
        {
            arrow = -1;
            xPos = 0;
            yPos = 0;
            ResetItem();
            rightMostPosition = 0;
        }

        public void SetSize(int _size) 
        { 
            myHoldSize = _size; 

            if(myHoldSize <= maxHold/2)
            {
                spaces[0] = new int[myHoldSize];
                spaces[1] = new int[0];
                spaces[2] = new int[shipHoldSize];
            }
            else
            {
                spaces[0] = new int[maxHold/2];
                spaces[1] = new int[myHoldSize - maxHold/2];
                spaces[2] = new int[shipHoldSize];
            }

            int id = 0;
            for(int ii = 0; ii < spaces.Length; ii++)
            {
                for(int jj = 0; jj < spaces[ii].Length; jj++)
                {
                    spaces[ii][jj] = id;
                    id++;
                } 
            }
        }

        public void ResetBuffer()
        {
            currentBuffer = 0;
        }

        public void IncreaseFrameBuffer()
        {
            currentBuffer = Mathf.Min(currentBuffer + 1, maxFrameBuffer);
        }
        #endregion

        #region//Navigation
        public void Navigation()
        {
            if(currentBuffer < maxFrameBuffer)
            {
                im.landedMenuSystem.ExpendAllDir();
                im.menuSystem.ExpendAllDir();
            }

            int startX = xPos;
            xPos = XNavigate(xPos);

            if(startX != xPos) 
                rightMostPosition = xPos;

            if(!forShip)
            {
                yPos = YNavigate(yPos);
                xPos = rightMostPosition;

                if(xPos >= spaces[yPos].Length)
                    xPos = spaces[yPos].Length - 1;
            }

            arrow = spaces[yPos][xPos];
        }

        int XNavigate(int _startXPos)
        {
            int xMove = 0;
            if (im.menuSystem.xNav != 0) 
                xMove = im.menuSystem.xNav;
            else if(im.landedMenuSystem.xNav != 0)
                xMove = im.landedMenuSystem.xNav;
            else 
                return _startXPos;

            int newXPos = _startXPos + xMove;
            im.menuSystem.ExpendXDir();
            im.landedMenuSystem.ExpendXDir();

            if(newXPos < 0)
                return 0;
            else if(newXPos > spaces[yPos].Length - 1)
                return spaces[yPos].Length - 1;
            
            return newXPos;
        }

        int YNavigate(int _startYPos)
        {
            int yMove = 0;
            if (im.menuSystem.yNav != 0)
                yMove = im.menuSystem.yNav;
            else if(im.landedMenuSystem.yNav != 0)
                yMove = im.landedMenuSystem.yNav;
            else 
                return _startYPos;

            int newYPos = _startYPos - yMove;
            im.menuSystem.ExpendYDir();
            im.landedMenuSystem.ExpendYDir();

            if(newYPos < 0)
                newYPos = 0;
            else if(newYPos >= spaces.Length)
                newYPos = spaces.Length - 1;
            else if(newYPos == 1 && myHoldSize <= maxHold/2)
                newYPos = (yMove < 0 ? 2 : 0);

            return newYPos;
        }
        #endregion

        #region //Selection
        public void ResetItem()
        {
            im.menuSystem.ExpendMenuSelect();
            im.landedMenuSystem.ExpendMenuSelect();
            activeItem = -99;
        }

        public void ResetArrow() 
        {
            arrow = 0; 
            xPos = 0;
            yPos = 0;
            rightMostPosition = 0;
        }

        public void Selection()
        {
            if (im.menuSystem.selected)
            {
                im.menuSystem.ExpendMenuSelect();
                im.landedMenuSystem.ExpendMenuSelect();
                if(!activeItemSet)
                    activeItem = arrow;
                else if (arrow == activeItem)
                    ResetItem();
            }
        }
        #endregion
    }
}