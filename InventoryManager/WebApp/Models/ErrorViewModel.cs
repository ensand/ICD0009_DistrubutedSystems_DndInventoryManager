using System;

namespace WebApp.Models
{
    /// <summary>
    /// Model for displaying errors
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// ID of the request
        /// </summary>
        public string RequestId { get; set; } = default!;

        /// <summary>
        /// If the ID of the request ID is showable
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}