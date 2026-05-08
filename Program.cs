using System;
using System.IO;
using System.Text;
namespace StudentGradingSystem
{
    public class Program
    {
        public enum EnPolicy
        {
            Percentage = 1,
            Weighted = 2
        }
        static void Main(string[] args)
        {
          Student student = new Student();
            //student.AddStudent();
            student.ViewFinalReportForStudent();
        }
    }
}
    