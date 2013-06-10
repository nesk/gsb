using System;
using System.Collections.Generic;
using System.Windows.Forms;

using gsb.Entities;

namespace gsb
{
    public partial class MainForm : Form
    {
        /*
         * Fields
         */

        bool ignoreEvents = false; // Used to avoid the Control_Changed method to do her work when a value is set programmatically

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

        private void removeExpenseOPButton_Click(object sender, EventArgs e)
        {
            ExpenseNote expenseNote = (ExpenseNote)this.expensesSelect.SelectedItem;
            ExpenseOffPlan expenseOffPlan = (ExpenseOffPlan)this.expensesOPList.SelectedItem;
            int index = this.expensesOPList.SelectedIndex;

            this.expensesOPList.Items.Remove(expenseOffPlan);
            expenseNote.RemoveExpenseOffPlan(expenseOffPlan);

            if (this.expensesOPList.Items.Count == index)
                this.expensesOPList.SelectedIndex = index - 1;
            else
                this.expensesOPList.SelectedIndex = index;

            this.RefreshControlsAvailability();
        }

        private void saveExpenseButton_Click(object sender, EventArgs e)
        {
            ((ExpenseNote)this.expensesSelect.SelectedItem).Save();

            this.RefreshControlsAvailability();
        }

        private void Control_Changed(object sender, EventArgs e)
        {
            if (ignoreEvents) return; // See ignoreEvents declaration

            ExpenseNote expenseNote = (ExpenseNote)this.expensesSelect.SelectedItem;
            ExpenseOffPlan expenseOffPlan = (ExpenseOffPlan)this.expensesOPList.SelectedItem;

            if (sender is NumericUpDown)
            {
                NumericUpDown control = (NumericUpDown)sender;

                if (control == this.etpNum)
                    expenseNote.SetExpenseInPlan("ETP", (int)this.etpNum.Value);
                else if (control == this.kmNum)
                    expenseNote.SetExpenseInPlan("KM", (int)this.kmNum.Value);
                else if (control == this.nuiNum)
                    expenseNote.SetExpenseInPlan("NUI", (int)this.nuiNum.Value);
                else if (control == this.repNum)
                    expenseNote.SetExpenseInPlan("REP", (int)this.repNum.Value);
                else // control == this.expenseOPCostNum
                    expenseOffPlan.Cost = this.expenseOPCostNum.Value;
            }
            else if (sender is TextBox)
            {

                expenseOffPlan.Label = ((TextBox)sender).Text;
                this.expensesOPList.Items[this.expensesOPList.SelectedIndex] = expenseOffPlan;
            }
            else // sender is DateTimePicker
                expenseOffPlan.Date = ((DateTimePicker)sender).Value;

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
            int incrementValueForExpenseInPlan = isCurrentExpenseOnTheCurrentMonth ? 1 : 0;
            int incrementValueForCost = isCurrentExpenseOnTheCurrentMonth && isThereOffPlanExpenses ? 1 : 0;

            /*
             * Assignments
             */

            this.expensesSelect.Enabled = isThereExpenses;
            this.createExpenseButton.Enabled = !isFirstExpenseOnTheCurrentMonth;

            this.etpNum.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.etpNum.Increment = incrementValueForExpenseInPlan;

            this.kmNum.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.kmNum.Increment = incrementValueForExpenseInPlan;

            this.nuiNum.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.nuiNum.Increment = incrementValueForExpenseInPlan;

            this.repNum.ReadOnly = !isCurrentExpenseOnTheCurrentMonth;
            this.repNum.Increment = incrementValueForExpenseInPlan;

            this.expensesOPList.Enabled = isThereExpenses;
            this.addExpenseOPButton.Enabled = isCurrentExpenseOnTheCurrentMonth;
            this.removeExpenseOPButton.Enabled = isCurrentExpenseOnTheCurrentMonth && isThereOffPlanExpenses;

            this.expenseOPDate.Enabled = isCurrentExpenseOnTheCurrentMonth && isThereOffPlanExpenses;
            this.expenseOPLabelText.ReadOnly = !(isCurrentExpenseOnTheCurrentMonth && isThereOffPlanExpenses);

            this.expenseOPCostNum.ReadOnly = !(isCurrentExpenseOnTheCurrentMonth && isThereOffPlanExpenses);
            this.expenseOPCostNum.Increment = incrementValueForCost;

            this.saveExpenseButton.Enabled = !isExpenseSaved;
            this.cancelExpenseButton.Enabled = !isExpenseSaved;
        }

        private void LoadExpenseNote(ExpenseNote expense)
        {
            this.ignoreEvents = true; // See ignoreEvents declaration

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

            this.ignoreEvents = false; // See ignoreEvents declaration
        }

        private void LoadExpenseOffPlan(ExpenseOffPlan expense)
        {
            if (expense == null) return;

            this.ignoreEvents = true; // See ignoreEvents declaration

            this.expenseOPDate.Value = expense.Date;
            this.expenseOPLabelText.Text = expense.Label;
            this.expenseOPCostNum.Value = expense.Cost;

            this.ignoreEvents = false; // See ignoreEvents declaration
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
