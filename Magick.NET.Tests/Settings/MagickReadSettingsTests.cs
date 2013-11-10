﻿//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class MagickReadSettingsTests
	{
		//===========================================================================================
		private const string _Category = "MagickReadSettings";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Collection_Read()
		{
			using (MagickImageCollection collection = new MagickImageCollection())
			{
				MagickReadSettings settings = new MagickReadSettings();
				settings.Density = new MagickGeometry(150, 150);

				collection.Read(Files.RoseSparkleGIF, settings);

				Assert.AreEqual(150, collection[0].Density.Width);

				settings = new MagickReadSettings();
				settings.FrameIndex = 1;

				collection.Read(Files.RoseSparkleGIF, settings);

				Assert.AreEqual(1, collection.Count);

				settings = new MagickReadSettings();
				settings.FrameIndex = 1;
				settings.FrameCount = 2;

				collection.Read(Files.RoseSparkleGIF, settings);

				Assert.AreEqual(2, collection.Count);

				settings = null;
				collection.Read(Files.RoseSparkleGIF, settings);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Image_Exceptions()
		{
			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				MagickReadSettings settings = new MagickReadSettings();
				settings.FrameCount = 2;
				new MagickImage(Files.RoseSparkleGIF, settings);
			});
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Image_Read()
		{
			using (MagickImage image = new MagickImage())
			{
				MagickReadSettings settings = new MagickReadSettings();
				settings.Density = new MagickGeometry(150, 150);

				image.Read(Files.SnakewarePNG, settings);

				Assert.AreEqual(150, image.Density.Width);

				settings = null;
				image.Read(Files.ImageMagickJPG, settings);
			}

			using (MagickImage image = new MagickImage(Files.RoseSparkleGIF))
			{
				MagickImage imageA = new MagickImage();
				MagickImage imageB = new MagickImage();

				MagickReadSettings settings = new MagickReadSettings();

				imageA.Read(Files.RoseSparkleGIF, settings);
				Assert.AreEqual(image, imageA);

				settings = new MagickReadSettings();
				settings.FrameIndex = 1;

				imageA.Read(Files.RoseSparkleGIF, settings);
				Assert.AreNotEqual(image, imageA);

				imageB.Read(Files.RoseSparkleGIF + "[1]");
				Assert.AreEqual(imageA, imageB);

				settings = new MagickReadSettings();
				settings.FrameIndex = 2;

				imageA.Read(Files.RoseSparkleGIF, settings);
				Assert.AreNotEqual(image, imageA);

				imageB.Read(Files.RoseSparkleGIF + "[2]");
				Assert.AreEqual(imageA, imageB);

				ExceptionAssert.Throws<MagickOptionErrorException>(delegate()
				{
					settings = new MagickReadSettings();
					settings.FrameIndex = 3;

					imageA.Read(Files.RoseSparkleGIF, settings);
				});

				imageA.Dispose();
				imageB.Dispose();
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
