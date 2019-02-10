using System;

namespace AdotAqui.Models.ViewModels
{
    /// <summary>
    /// Error ViewModel used to represent a error
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorId { get; set; }

        public string Description { get; set; }

        public bool ShowDescription => !string.IsNullOrEmpty(Description);

    }
}