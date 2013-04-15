using System;
using System.Collections.Generic;
using System.Windows.Forms;

using gsb.Entities;

namespace gsb
{
    public partial class MainForm : Form
    {
        /*
         * Constructors
         */

        public MainForm()
        {
            InitializeComponent();
        }

        /*
         * Event handlers
         */

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.expensesSelect.Items.AddRange(Expenses.GetExpenseNotes().ToArray());

            if(this.expensesSelect.Items.Count > 0)
                this.expensesSelect.SelectedIndex = 0;
        }

        private void ListControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox list = (ComboBox)sender;
                this.LoadExpenseNote((ExpenseNote)list.SelectedItem);
            }
            else
            {
                ListBox list = (ListBox)sender;
                this.LoadExpenseOffPlan((ExpenseOffPlan)list.SelectedItem);
            }
        }

        /*
         * Methods
         */

        private void LoadExpenseNote(ExpenseNote expense)
        {
            this.etpText.Text = expense.ExpensesInPlan["ETP"].ToString();
            this.kmText.Text = expense.ExpensesInPlan["KM"].ToString();
            this.nuiText.Text = expense.ExpensesInPlan["NUI"].ToString();
            this.repText.Text = expense.ExpensesInPlan["REP"].ToString();

            ExpenseOffPlan[] expensesOffPlan = new ExpenseOffPlan[expense.ExpensesOffPlan.Count];
            expense.ExpensesOffPlan.CopyTo(expensesOffPlan, 0);
            this.expensesOPList.Items.Clear();
            this.expensesOPList.Items.AddRange(expensesOffPlan);

            if (this.expensesOPList.Items.Count > 0)
                this.expensesOPList.SelectedIndex = 0;
        }

        private void LoadExpenseOffPlan(ExpenseOffPlan expense)
        {
            this.expenseOPDate.Value = expense.Date;
            this.expenseOPLabelText.Text = expense.Label;
            this.expenseOPCostText.Text = expense.Cost.ToString();
        }
    }
}
