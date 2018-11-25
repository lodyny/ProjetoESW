using System;

namespace AdotAqui.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorId { get; set; }

        public string Description { get; set; }

        public bool ShowDescription => !string.IsNullOrEmpty(Description);

    }
}