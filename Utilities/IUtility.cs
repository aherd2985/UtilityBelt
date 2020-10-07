using Microsoft.Extensions.Options;
using System.Collections.Generic;
using UtilityBelt.Models;

namespace UtilityBelt
{
    /// <summary>
    /// An interface which must be implemented by every Utility to be properly
    /// handled by MEF
    /// </summary>
    public interface IUtility
    {
        /// <summary>
        /// Configure utility by passing the Secrets. May be not handled in the utility if not needed.
        /// </summary>
        /// <param name="options">Secrets to be used by implementing Utility</param>
        void Configure(IOptions<SecretsModel> options);

        /// <summary>
        /// The utility entry point - will be run when the user selects to run implementing utility
        /// </summary>
        void Run();

        /// <summary>
        /// List of text commands which will start implementing utility
        /// </summary>
        IList<string> Commands { get; }

        /// <summary>
        /// Name of the utility visible in the main menu
        /// </summary>
        string Name { get; }
    }
}