﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.EncryptServices.Settings
{
    public interface ICryptographySetting
    {
        string Key { get; set; }
    }
    public class CryptographySetting : ICryptographySetting
    {
        public string Key { get; set; }
    }
}
