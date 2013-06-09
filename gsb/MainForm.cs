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

            // The increment value is used to nullify the action of the buttons next to a NumericUpDown control when it's readonly
            int incrementValueforNumericBoxes = isCurrentExpenseOnTheCurrentMonth ? 1 : 0;

            /*
             * Assignments
             */

            this.expensesSelect.Enabled = isThereExpenses;
            this.createExpenseButton.Enabled = !isFirstExpenseOnTheCurrentMonth;

            this.etpNum.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.etpNum.Increment = incrementValueforNumericBoxes;

            this.kmNum.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.kmNum.Increment = incrementValueforNumericBoxes;

            this.nuiNum.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.nuiNum.Increment = incrementValueforNumericBoxes;

            this.repNum.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.repNum.Increment = incrementValueforNumericBoxes;

            this.expensesOPList.Enabled = isThereExpenses;
            this.addExpenseOPButton.Enabled = isCurrentExpenseOnTheCurrentMonth;
            this.removeExpenseOPButton.Enabled = isCurrentExpenseOnTheCurrentMonth && isThereOffPlanExpenses;

            this.expenseOPDate.Enabled = isCurrentExpenseOnTheCurrentMonth;
            this.expenseOPLabelText.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;

            this.expenseOPCostNum.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.expenseOPCostNum.Increment = incrementValueforNumericBoxes;

            this.saveExpenseButton.Enabled = !isExpenseSaved;
            this.cancelExpenseButton.Enabled = !isExpenseSaved;
        }

        private void LoadExpenseNote(ExpenseNote expense)
        {
            this.stateLabel.Text = String.Format("Etat : {0}", expense.State);
            this.approvedAmountLabel.Text = String.Format("Montant approuvé : {0:C}", expense.ApprovedAmount);
            this.vouchersLabel.Text = String.Format("Nombre de justificatifs reçus : {0}", expense.VouchersNb);

            this.etpNum.Value = expense.ExpensesInPlan["ETP"];
            this.kmNum.Value = expense.ExpensesInPlan["KM"];
            this.nuiNum.Value = expense.ExpensesInPlan["NUI"];
            this.repNum.Value = expense.ExpensesInPlan["REP"];

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
            this.expenseOPCostNum.Value = expense.Cost;
        }

        private void ClearExpensesInPlan()
        {
            this.etpNum.Value = 0;
            this.kmNum.Value = 0;
            this.nuiNum.Value = 0;
            this.repNum.Value = 0;
        }

        private void ClearExpensesOffPlan()
        {
            this.expensesOPList.Items.Clear();
            this.expenseOPDate.Value = DateTime.Today;
            this.expenseOPLabelText.Text = "";
            this.expenseOPCostNum.Value = 0;
        }
    }
}
