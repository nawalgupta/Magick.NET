﻿// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

#if !NETCORE

using System.Web;
using ImageMagick.Web;
using ImageMagick.Web.Handlers;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    internal sealed class TestMagickHandler : MagickHandler
    {
        public TestMagickHandler(MagickWebSettings settings, IImageData imageData)
          : base(settings, imageData)
        {
            FileName = Files.MagickNETIconPNG;
        }

        public string FileName { get; set; }

        protected override string GetFileName(HttpContext context)
        {
            return FileName;
        }
    }
}

#endif