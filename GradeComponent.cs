using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGradingSystem
{
    public class GradeComponent
    {
        protected string _ComponentName;
        protected double _Score ;
        protected double _MaxScore;
        protected double _Weight;


        // Property
        public string ComponentName
        {
            get { return _ComponentName; } 
            set { _ComponentName = value; }
        }
        public double Weight
        {
            get { return _Weight; } 
            set { 
                if (value > 0)
                    _Weight = value; 
                else 
                    _Weight = 0;
            }
        }
        public double Score
        {
            get { return _Score; } 
            set {
                if (value > 0)
                  _Score = value;
                else 
                   _Score = 25;
            }
        }
        public double MaxScore
        {
            get { return _MaxScore; } 
            set
            {
                if (value > 0)
                    _MaxScore = value;
                else
                    _MaxScore = 50;
            }
        }


        // constructors
        public GradeComponent()
        {
            _MaxScore = 0;
            _Score = 0;
            _ComponentName = "";
            _Weight = 0; 
        }
        public GradeComponent(string componentname, double score, double maxscore, double weight)
        {
            MaxScore = maxscore;
            Score = score;
            ComponentName = componentname;
            _Weight = weight;
        }
    }
}
