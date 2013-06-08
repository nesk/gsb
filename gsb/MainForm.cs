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
            List<ExpenseNote> expenseNotes = Expenses.GetExpenseNotes();
            expenseNotes.Sort();
            this.expensesSelect.Items.AddRange(expenseNotes.ToArray());

            if(this.expensesSelect.Items.Count > 0)
                this.expensesSelect.SelectedIndex = 0;

            this.RefreshControlsAvailability();
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

            this.RefreshControlsAvailability();
        }

        private void createExpenseButton_Click(object sender, EventArgs e)
        {
            this.expensesSelect.Items.Insert(0, new ExpenseNote());
            this.expensesSelect.SelectedIndex = 0;

            this.RefreshControlsAvailability();
        }

        private void addExpenseOPButton_Click(object sender, EventArgs e)
        {
            ExpenseNote expense = (ExpenseNote)this.expensesSelect.SelectedItem;

            this.expensesOPList.Items.Add(expense.AddExpenseOffPlan());
            this.expensesOPList.SelectedIndex = this.expensesOPList.Items.Count - 1;
            this.expenseOPLabelText.Focus();

            this.RefreshControlsAvailability();
        }

        private void saveExpenseButton_Click(object sender, EventArgs e)
        {
            ((ExpenseNote)this.expensesSelect.SelectedItem).Save();

            this.RefreshControlsAvailability();
        }

        /*
         * Methods
         */

        private void RefreshControlsAvailability()
        {
            /*
             * Initialization
             */

            bool isThereExpenses = (this.expensesSelect.Items.Count > 0);

            ExpenseNote currentExpense = isThereExpenses ? ((ExpenseNote)this.expensesSelect.SelectedItem) : null;
            ExpenseNote firstExpense = isThereExpenses ? ((ExpenseNote)this.expensesSelect.Items[0]) : null;

            bool isFirstExpenseOnTheCurrentMonth = (firstExpense != null) ? (firstExpense.Date.Month == DateTime.Today.Month) : false;
            bool isCurrentExpenseOnTheCurrentMonth = (currentExpense != null) ? (currentExpense.Date.Month == DateTime.Today.Month) : false;
            bool isThereOffPlanExpenses = (this.expensesOPList.Items.Count > 0);
            bool isExpenseSaved = (currentExpense != null) ? (currentExpense.Status == ExpenseState.Loaded) : true;

            /*
             * Assignments
             */

            this.expensesSelect.Enabled = isThereExpenses;
            this.createExpenseButton.Enabled = !isFirstExpenseOnTheCurrentMonth;

            this.etpText.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.kmText.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.nuiText.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.repText.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;

            this.expensesOPList.Enabled = isThereExpenses;
            this.addExpenseOPButton.Enabled = isCurrentExpenseOnTheCurrentMonth;
            this.removeExpenseOPButton.Enabled = isCurrentExpenseOnTheCurrentMonth && isThereOffPlanExpenses;

            this.expenseOPDate.Enabled = isCurrentExpenseOnTheCurrentMonth;
            this.expenseOPLabelText.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.expenseOPCostText.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;

            this.saveExpenseButton.Enabled = !isExpenseSaved;
            this.cancelExpenseButton.Enabled = !isExpenseSaved;
        }

        private void LoadExpenseNote(ExpenseNote expense)
        {
            this.etpText.Text = expense.ExpensesInPlan["ETP"].ToString();
            this.kmText.Text = expense.ExpensesInPlan["KM"].ToString();
            this.nuiText.Text = expense.ExpensesInPlan["NUI"].ToString();
            this.repText.Text = expense.ExpensesInPlan["REP"].ToString();

            ExpenseOffPlan[] expensesOffPlan = new ExpenseOffPlan[expense.ExpensesOffPlan.Count];
            expense.ExpensesOffPlan.CopyTo(expensesOffPlan, 0);
            Array.Sort(expensesOffPlan);
            this.ClearExpensesOffPlan();
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

        private void ClearExpensesInPlan()
        {
            this.etpText.Text = "";
            this.kmText.Text = "";
            this.nuiText.Text = "";
            this.repText.Text = "";
        }

        private void ClearExpensesOffPlan()
        {
            this.expensesOPList.Items.Clear();
            this.expenseOPDate.Value = DateTime.Today;
            this.expenseOPLabelText.Text = "";
            this.expenseOPCostText.Text = "";
        }
    }
}
