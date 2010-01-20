using System;
using Jamaica.TableFootball.Core.Xhtml;

namespace Jamaica.TableFootball.Core.PasswordReset
{
    public class PasswordResetResource
    {
        public SelectList Users { get; set; }
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }
}