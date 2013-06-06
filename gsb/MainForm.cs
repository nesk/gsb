﻿using System;
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
            bool isThereExpenses = (this.expensesSelect.Items.Count > 0);
            ExpenseNote expense = isThereExpenses ? ((ExpenseNote)this.expensesSelect.SelectedItem) : null;
            bool isExpenseOnTheCurrentMonth = (expense != null) ? (expense.Date.Month == DateTime.Today.Month) : false;
            bool isThereOffPlanExpenses = (this.expensesOPList.Items.Count > 0);
            bool isExpenseSaved = (expense != null) ? (expense.Status == ExpenseState.Loaded) : true;

            this.expensesSelect.Enabled = isThereExpenses;
            this.createExpenseButton.Enabled = !isExpenseOnTheCurrentMonth;

            this.etpText.ReadOnly = !isExpenseOnTheCurrentMonth;
            this.kmText.ReadOnly = !isExpenseOnTheCurrentMonth;
            this.nuiText.ReadOnly = !isExpenseOnTheCurrentMonth;
            this.repText.ReadOnly = !isExpenseOnTheCurrentMonth;

            this.expensesOPList.Enabled = isThereExpenses;
            this.addExpenseOPButton.Enabled = isExpenseOnTheCurrentMonth;
            this.removeExpenseOPButton.Enabled = isExpenseOnTheCurrentMonth && isThereOffPlanExpenses;

            this.expenseOPDate.Enabled = isExpenseOnTheCurrentMonth;
            this.expenseOPLabelText.ReadOnly = !isExpenseOnTheCurrentMonth;
            this.expenseOPCostText.ReadOnly = !isExpenseOnTheCurrentMonth;

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
