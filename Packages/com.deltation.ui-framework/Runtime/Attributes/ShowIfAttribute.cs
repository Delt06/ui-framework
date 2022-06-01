using System;
using JetBrains.Annotations;
using UnityEngine;

namespace DELTation.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class ShowIfAttribute : PropertyAttribute, IConditionShowAttribute
    {
        public ShowIfAttribute([NotNull] string memberName) =>
            MemberName = memberName ?? throw new ArgumentNullException(nameof(memberName));

        public string MemberName { get; }
    }
}