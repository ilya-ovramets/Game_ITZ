using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boomber
{
    class BombCell
    {
        private int x;
        private int y;
        public Button but;
        public bool visibl = false;
        private bool isBoomb;
        public int readCell = 0;

        public BombCell(int x,int y,bool isBoomb, Button btn)
        {
            this.isBoomb = isBoomb;
            this.x = x;
            this.y = y;
            this.but = btn;
        }

        public bool IsBoomb()
        {
            return isBoomb;
        }
    }
}
