﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJingFramework.Painting.Deriving.Extensions;
using YiJingFramework.References.Zhouyi;

namespace YiJingFramework.References.Zhouyi.Tests
{
    [TestClass()]
    public class ZhouyiHexagramTests
    {
        private readonly Zhouyi yijing = new Zhouyi();

        public ZhouyiHexagramTests()
        {
            this.qian = this.yijing.GetHexagram(new Core.Painting(
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yang
            ));
            this.weiJi = this.yijing.GetHexagram(new Core.Painting(
                Core.LineAttribute.Yin,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yin,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yin,
                Core.LineAttribute.Yang
            ));
            _ = this.yijing.TryGetHexagram("师", out this.shi);
        }
        private readonly ZhouyiHexagram qian;
        private readonly ZhouyiHexagram shi;
        private readonly ZhouyiHexagram weiJi;

        [TestMethod()]
        public void PropertiesTest()
        {
            Assert.IsNull(this.shi.ApplyNinesOrApplySixes);
            Assert.AreEqual($"见群龙无首，吉。", this.qian.ApplyNinesOrApplySixes);
            Assert.AreEqual("利永贞。",
                this.yijing.GetHexagram(this.qian.GetPainting().ToLaterallyLinked())
                .ApplyNinesOrApplySixes);
            Assert.AreEqual(64, this.weiJi.Index);
            Assert.IsTrue(yijing.TryGetTrigram("坎", out var kan));
            Assert.AreEqual(kan, this.weiJi.LowerTrigram);
            Assert.AreEqual(
                this.yijing.GetHexagram(this.qian.GetPainting().ToLaterallyLinked()).UpperTrigram
                , this.shi.UpperTrigram);
            Assert.AreEqual("未济", weiJi.Name);

            Assert.AreEqual("元，亨，利，贞。", qian.Text);
        }

        [TestMethod()]
        public void GetPaintingTest()
        {
            Assert.AreEqual("010101", this.weiJi.GetPainting().ToString());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsFalse(this.qian.Equals(this.weiJi));
            Assert.IsFalse(this.qian.Equals(null));
            Assert.IsFalse(this.qian.Equals(new object()));
            Assert.IsTrue(this.shi.Equals(new Zhouyi().GetHexagram(new Core.Painting(
                Core.LineAttribute.Yin,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yin,
                Core.LineAttribute.Yin,
                Core.LineAttribute.Yin,
                Core.LineAttribute.Yin
            ))));
            Assert.IsTrue(this.shi.Equals((object)new Zhouyi().GetHexagram(new Core.Painting(
                Core.LineAttribute.Yin,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yin,
                Core.LineAttribute.Yin,
                Core.LineAttribute.Yin,
                Core.LineAttribute.Yin
            ))));
            Assert.IsTrue(this.qian.Equals((object)this.qian));
            Assert.IsTrue(this.qian == new Zhouyi().GetHexagram(new Core.Painting(
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yang,
                Core.LineAttribute.Yang
            )));
            Assert.IsFalse(this.qian == this.shi);
            Assert.IsTrue(this.qian != this.weiJi);
            Assert.IsFalse(this.qian == null);
            Assert.IsTrue(this.qian != null);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Assert.IsTrue(this.qian.CompareTo(this.shi) < 0);
            Assert.IsTrue(this.qian.CompareTo(null) > 0);
            Assert.IsTrue(this.weiJi.CompareTo(this.qian) > 0);
            Assert.IsTrue(this.qian.CompareTo(this.qian) == 0);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Assert.AreEqual(1, this.qian.GetHashCode());
            Assert.AreEqual(64, this.weiJi.GetHashCode());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual($"第一卦 " +
                $"乾 乾为天 " +
                $"乾上乾下{Environment.NewLine}" +
                $"元，亨，利，贞。{Environment.NewLine}" +
                $"初九：潜龙，勿用。{Environment.NewLine}" +
                $"九二：见龙再田，利见大人。{Environment.NewLine}" +
                $"九三：君子终日乾乾，夕惕若，厉无咎。{Environment.NewLine}" +
                $"九四：或跃在渊，无咎。{Environment.NewLine}" +
                $"九五：飞龙在天，利见大人。{Environment.NewLine}" +
                $"上九：亢龙有悔。{Environment.NewLine}" +
                $"用九：见群龙无首，吉。{Environment.NewLine}", this.qian.ToString());
            Assert.AreEqual($"第六十四卦 " +
                $"未济 火水未济 " +
                $"离上坎下{Environment.NewLine}" +
                $"亨，小狐汔济，濡其尾，无攸利。{Environment.NewLine}" +
                $"初六：濡其尾，吝。{Environment.NewLine}" +
                $"九二：曳其轮，贞吉。{Environment.NewLine}" +
                $"六三：未济，征凶，利涉大川。{Environment.NewLine}" +
                $"九四：贞吉，悔亡，震用伐鬼方，三年有赏于大国。{Environment.NewLine}" +
                $"六五：贞吉，无悔，君子之光，有孚，吉。{Environment.NewLine}" +
                $"上九：有孚于饮酒，无咎，濡其首，有孚失是。{Environment.NewLine}"
                , this.weiJi.ToString());
        }
    }
}