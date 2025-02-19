﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Model
{
    public class BallInModel : IBall
    {
        public int Diameter { get;}
        private double top;
        private double left;
        public event PropertyChangedEventHandler PropertyChanged;

        public BallInModel(double top, double left, int radius)
        {
            Top = top;
            Left = left;
            Diameter = radius * 2;
        }


        public double Top
        {
            get { return top; }
            set 
            {
                if (top == value)
                    return;
                top = value;
                RaisePropertyChanged();
            }
        }

        public double Left
        {
            get { return left; }
            set
            {
                if (left == value)
                    return;
                left = value;
                RaisePropertyChanged();
            }
        }

        public void Move(double poitionX, double positionY)
        {
           Left = poitionX;
           Top = positionY;
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

