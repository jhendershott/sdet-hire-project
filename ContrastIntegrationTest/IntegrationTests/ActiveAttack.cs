using ContrastIntegrationTest.Flows;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ContrastIntegrationTest.IntegrationTests
{
    public class ActiveAttack
    {
        [Fact]
        public void ActiveAttackIncreasesCount()
        {
            var prevAttacks = AttackFlows.GetActiveAttacks();
            //new attack
            AttackFlows.XssInjectionAttack();

            var newAttacks = AttackFlows.GetActiveAttacks();

            Assert.True(AttackFlows.WaitForAttackUpdate(prevAttacks), "Attack was not registered");
        }
    }
}
