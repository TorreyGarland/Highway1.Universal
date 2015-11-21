namespace Highway1.Universal.Services
{

    partial class Navigator
    {

        /// <summary>Loads the state of the view.</summary>
        /// <param name="state">The state.</param>
        public void LoadViewState(string state) 
            => _frame?.SetNavigationState(state);

        /// <summary>Saves the state of the view.</summary>
        /// <returns></returns>
        public string SaveViewState()
            => _frame?.GetNavigationState();

    }

}