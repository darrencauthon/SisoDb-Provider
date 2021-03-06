﻿using System;

namespace SisoDb.Structures.Schemas.MemberAccessors
{
    internal abstract class MemberAccessorBase : IMemberAccessor
    {
        protected IProperty Property { get; private set; }

        public string Name { get; private set; }

        public string Path
        {
            get { return Property.Path; }
        }

        public Type DataType
        {
            get { return Property.PropertyType; }
        }

        protected MemberAccessorBase(IProperty property)
        {
            Property = property;
            Name = SisoDbEnvironment.MemberNameGenerator.Generate(property.Path);
        }
    }
}