namespace Highway1.Universal.ViewModels
{

    using ComponentModel;

    /// <summary>Activatable interface.</summary>
    public interface IActivatable
    {

        /// <summary>Activates the specified e.</summary>
        /// <param name="e">The <see cref="NavigatorEventArgs" /> instance containing the event data.</param>
        void Activate(NavigatorEventArgs e);

    }

}