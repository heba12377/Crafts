﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crafts.BL.Dtos.IdentityDtos
{
    public record ResetPasswordDto(string Email, string Password);
    
}