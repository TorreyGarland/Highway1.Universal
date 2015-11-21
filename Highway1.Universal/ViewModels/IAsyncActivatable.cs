namespace Highway1.Universal.ViewModels
{

    using ComponentModel;
    using System.Threading.Tasks;

    /// <summary>Asynchronous activatable interface.</summary>
    public interface IAsyncActivatable
    {

        /// <summary>Activates the asynchronous.</summary>
        /// <param name="e">The <see cref="NavigatorEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        Task ActivateAsync(NavigatorEventArgs e);

    }

}