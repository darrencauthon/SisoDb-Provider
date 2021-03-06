﻿using System;
using SisoDb.Resources;
using SisoDb.Structures.Schemas.MemberAccessors;

namespace SisoDb.Providers.SqlProvider.DbSchema
{
    internal class SqlDbDataTypeTranslator
    {
        internal string ToDbType(IIndexAccessor indexAccessor)
        {
            if(indexAccessor.IsEnumerable && indexAccessor.DataType.IsEnumerableBytesType())
                throw new SisoDbException(
                    ExceptionMessages.SqlDbDataTypeTranslator_ByteArraysAreNotSupported.Inject(indexAccessor.Path));

            if(indexAccessor.IsElement)
                return ("[nvarchar](max)");

            return ToDbType(indexAccessor.DataType);
        }

        private static string ToDbType(Type dataType)
        {
            if (dataType.IsEnumerableType())
                return ("[nvarchar](max)");

            if (dataType.IsStringType())
                return ("[nvarchar](max)");

            if (dataType.IsIntType() || dataType.IsNullableIntType())
                return ("[int]");

            if (dataType.IsByteType() || dataType.IsNullableByteType())
                return ("[tinyint]");

            if (dataType.IsDateTimeType() || dataType.IsNullableDateTimeType())
                return ("[datetime]");

            if (dataType.IsLongType() || dataType.IsNullableLongType())
                return ("[bigint]");

            if (dataType.IsShortType() || dataType.IsNullableShortType())
                return ("[smallint]");

            if (dataType.IsBoolType() || dataType.IsNullableBoolType())
                return ("[bit]");

            if (dataType.IsGuidType() || dataType.IsNullableGuidType())
                return ("[uniqueidentifier]");

            if (dataType.IsDecimalType() || dataType.IsNullableDecimalType())
                return ("[decimal](18,5)");

            if (dataType.IsDoubleType() || dataType.IsNullableDoubleType())
                return ("[float]");

            if (dataType.IsSingleType() || dataType.IsNullableSingleType())
                return ("[float]");

            if (dataType.IsFloatType() || dataType.IsNullableFloatType())
                return ("[float]");

            if (dataType.IsCharType() || dataType.IsNullableCharType())
                return ("[nchar](1)");

            if (dataType.IsEnumType() || dataType.IsNullableEnumType())
                return ("[int]");
            
            throw new SisoDbException(
                ExceptionMessages.SqlDbDataTypeTranslator_UnsupportedDataType.Inject(dataType.Name));
        }
    }
}