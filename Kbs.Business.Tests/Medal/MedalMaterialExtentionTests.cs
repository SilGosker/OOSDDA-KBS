﻿using Kbs.Business.Boat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Medal
{
    public class MedalMaterialExtentionTests
    {
        [Theory]
        [InlineData(MedalMaterial.Bronze, "Brons")]
        [InlineData(MedalMaterial.Silver, "Silver")]
        [InlineData(MedalMaterial.Gold, "Goud")]
        [InlineData((MedalMaterial)int.MaxValue, "Onbekend")]
        [InlineData((MedalMaterial)0, "Onbekend")]
        public void ToDutchString_ShouldReturnString(MedalMaterial material, string expected)
        {
            // Act
            var actual = material.ToDutchString();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}