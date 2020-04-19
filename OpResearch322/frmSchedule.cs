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
    public partial class frmSchedule : Form
    {
        public frmSchedule()
        {
            InitializeComponent();
        }

        private void frmSchedule_Load(object sender, EventArgs e)
        {
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
           //convert string values from text box to integer values
          //so it gets Accepted as parameters for demand in next step

            var A = int.Parse("textBox1.Text");
            var B = int.Parse("textBox2.Text");
            var C = int.Parse("textBox3.Text");
            var D = int.Parse("textBox4.Text");
            var E = int.Parse("textBox5.Text");
            var F = int.Parse("textBox6.Text");

            Solver solver = new Solver("Schedule");

            //
            // data
            //
            int time_slots = 6;
            int[] demands = { A, B, C, D, E, F };
            int max_num = demands.Sum();

            //
            // Decision variables
            //

            // How many employee start the schedule at time slot t
            IntVar[] x = solver.MakeIntVarArray(time_slots, 0, max_num, "x");
            // Total number of employee
            IntVar num_employee = x.Sum().VarWithName("num_employee");

            //
            // Constraints
            //

            // Meet the demands for this and the next time slot.
            for (int i = 0; i < time_slots - 1; i++)
            {
                solver.Add(x[i] + x[i + 1] >= demands[i]);
            }

            // The demand "around the clock"
            solver.Add(x[time_slots - 1] + x[0] - demands[time_slots - 1] == 0);

            // For showing all solutions of minimal number of employee
            if (num_employee_check > 0)
            {
                solver.Add(num_employee == num_employee_check);
            }


            //
            // Search
            //
            DecisionBuilder db = solver.MakePhase(x,
                                                  Solver.CHOOSE_FIRST_UNBOUND,
                                                  Solver.ASSIGN_MIN_VALUE);

            if (num_employee_check == 0)
            {

                // Minimize num_employee
                OptimizeVar obj = num_employee.Minimize(1);
                solver.NewSearch(db, obj);

            }
            else
            {

                solver.NewSearch(db);

            }

            long result = 0;
            while (solver.NextSolution())
            {
                result = num_employee.Value();
                Console.Write("x: ");
                for (int i = 0; i < time_slots; i++)
                {
                    Console.Write("{0,2} ", x[i].Value());
                }
                Console.WriteLine("num_employee: " + num_employee.Value());
            }

            Console.WriteLine("\nSolutions: {0}", solver.Solutions());
            Console.WriteLine("WallTime: {0}ms", solver.WallTime());
            Console.WriteLine("Failures: {0}", solver.Failures());
            Console.WriteLine("Branches: {0} ", solver.Branches());

            solver.EndSearch();

            return result;

        }



        public static void Main(String[] args)
        {

            Console.WriteLine("Check for minimum number of employee: ");
            long num_employee = Solve();
            Console.WriteLine("\n... got {0} as minimal value.", num_employee);
            Console.WriteLine("\nAll solutions: ", num_employee);
            num_employee = Solve(num_employee);

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
