using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGradingSystem
{
    public class Course
    {
        protected string _CourseCode;
        protected string _CourseName;
        protected int _CreditHours;
        public List<GradeComponent> _GradeComponent = new List<GradeComponent>();
        

        // property
        public string CourseCode
        {
            get { return _CourseCode; }
            set
            {
                _CourseCode = value;
            }
        }
        public string CourseName
        {
            get { return _CourseName; }
            set
            {
                _CourseName = value;
            }
        }
        public int CreditHours
        {
            get { return _CreditHours; }
            set
            {
                if (value >= 0)
                    _CreditHours = value;
                else 
                    _CreditHours = 0;
            }
        }


        // constructors
        public Course()
        {
            _CourseName = "";
            _CreditHours = 0;
            _CourseCode = "";
        }
        public Course(string coursecode , string coursename , int credithours , List<GradeComponent> gradecomponents)
        {
            CourseCode=coursecode;
            CourseName=coursename;
            CreditHours=credithours;
            _GradeComponent = gradecomponents;
        }
        public Course(string coursecode, string coursename, int credithours)
        {
            CourseCode = coursecode;
            CourseName = coursename;
            CreditHours = credithours;
        }


        /* calculate total grade */
        public double CalculateTotalGrade()
        {
            double totalGrade = 0;
            foreach ( var component in _GradeComponent)
            {
                totalGrade += component.Score;
            }
            return totalGrade;
        }
    }
}
