﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Numerics;

namespace Logic
{
    public class CollisionControler
    {
        private int mass;
        private int radius;
        private Vector2 position;
        private Vector2 velocity;

        public CollisionControler(double poitionX, double poitionY, double speedX, double speedY, int radious, int mass)
        {
            this.velocity = new Vector2(speedX, speedY);
            this.position = new Vector2(poitionX, poitionY);
            this.radius = radious;
            this.mass = mass;
        }

        public bool IsCollision(double otherX, double otherY, double otherRadius, bool mode)
        {
            double currentX;
            double currentY;
            if (mode)
            {
                currentX = position.X + velocity.X;
                currentY = position.Y + velocity.Y;
            } 
            else
            {
                currentX = position.X;
                currentY = position.Y;
            }

            double distance = Math.Sqrt(Math.Pow(currentX - otherX, 2) + Math.Pow(currentY - otherY, 2));

            if (Math.Abs(distance) <= radius + otherRadius)
            {
                return true;
            }

            return false;
        }

        public bool IsTouchingBoundariesX(int boardSize)
        {
            double newX = position.X + velocity.X;
                
            if ((newX > boardSize && velocity.X > 0) || (newX < 0 && velocity.X < 0))
            {
                return true;
            }
            return false;
        }

        public bool IsTouchingBoundariesY(int boardSize)
        {
            double newY = position.Y + velocity.Y;
            if ((newY > boardSize && velocity.Y > 0) || (newY < 0 && velocity.Y < 0))
            {
                return true;
            }
            return false;
        }

        public Vector2[] ImpulseSpeed(double otherX, double otherY, double speedX, double speedY, double otherMass) 
        {
            Vector2 velocityOther = new Vector2(speedX, speedY);
            Vector2 positionOther = new Vector2(otherX, otherY);

            double fDistance = Math.Sqrt((position.X - positionOther.X) * (position.X - positionOther.X) + (position.Y - positionOther.Y) * (position.Y - positionOther.Y));

            double nx = (positionOther.X - position.X) / fDistance;
            double ny = (positionOther.Y - position.Y) / fDistance;

            double tx = -ny;
            double ty = nx;

            // Dot Product Tangent
            double dpTan1 = velocity.X * tx + velocity.Y * ty;
            double dpTan2 = velocityOther.X * tx + velocityOther.Y * ty;

            // Dot Product Normal
            double dpNorm1 = velocity.X * nx + velocity.Y * ny;
            double dpNorm2 = velocityOther.X * nx + velocityOther.Y * ny;

            // Conservation of momentum in 1D
            double m1 = (dpNorm1 * (mass - otherMass) + 2.0f * otherMass * dpNorm2) / (mass + otherMass);
            double m2 = (dpNorm2 * (otherMass - mass) + 2.0f * mass * dpNorm1) / (mass + otherMass);

            return new Vector2[2] { new Vector2(tx * dpTan1 + nx * m1, ty * dpTan1 + ny * m1), new Vector2(tx * dpTan2 + nx * m2, ty * dpTan2 + ny * m2) };

        }
    }
}
