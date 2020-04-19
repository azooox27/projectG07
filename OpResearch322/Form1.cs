using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.OrTools.LinearSolver;

namespace OpResearch322
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create the linear solver with the GLOP backend.
            Solver solver = Solver.CreateSolver("SimpleLpProgram", "GLOP_LINEAR_PROGRAMMING");

            // Create the variables x and y.
            Variable x = solver.MakeNumVar(0.0, 1.0, "x");
            Variable y = solver.MakeNumVar(0.0, 2.0, "y");

            Console.WriteLine("Number of variables = " + solver.NumVariables());

            // Create a linear constraint, 0 <= x + y <= 2.
            Google.OrTools.LinearSolver.Constraint ct = solver.MakeConstraint(0.0, 2.0, "ct");
            ct.SetCoefficient(x, 1);
            ct.SetCoefficient(y, 1);

            Console.WriteLine("Number of constraints = " + solver.NumConstraints());

            // Create the objective function, 3 * x + y.
            Objective objective = solver.Objective();
            objective.SetCoefficient(x, 3);
            objective.SetCoefficient(y, 1);
            objective.SetMaximization();

            solver.Solve();
            string str1, str2, str3, str4;
            str1 = "Solution:";
            str2 = "Objective value = " + solver.Objective().Value();
            str3 = "x = " + x.SolutionValue();
            str4 = "y = " + y.SolutionValue();

            label1.Text = str1;
            label2.Text = str2;
            label3.Text = str3;
            label4.Text = str4;

        }
    }
}
