using System;
using System.Collections;
using System.Collections.Generic;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;

namespace CmsApiSamples.Protocol.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IBuilder"/> interface.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Merges the specified source message in the message being built.
        /// </summary>
        /// <param name="builder">Instance of builder, which message is building.</param>
        /// <param name="source">Source message, which fields will be merged from.</param>
        /// <param name="fieldValueMapper">Value mapper that allows to change source message field values.</param>
        /// <returns></returns>
        public static IBuilder MergeFrom(this IBuilder builder, IMessage source, Func<FieldDescriptor, object, object> fieldValueMapper)
        {
            foreach (KeyValuePair<FieldDescriptor, object> pair in source.AllFields)
            {
                FieldDescriptor field = pair.Key;
                object currentValue = pair.Value;

                if (field.IsRepeated)
                {
                    foreach (object v in (IEnumerable)currentValue)
                    {
                        object newValue = mapValue(builder, field, v, fieldValueMapper);
                        builder.WeakAddRepeatedField(field, newValue);
                    }
                }
                else
                {
                    object newValue = mapValue(builder, field, currentValue, fieldValueMapper);
                    builder.SetField(field, newValue);
                }
            }

            return builder;
        }

        /// <summary>
        /// Call setter if specified value is not null.
        /// </summary>
        public static TBuilder SetIfNotNull<TBuilder, TProperty>(this TBuilder builder, TProperty? value, Func<TProperty, TBuilder> setter)
            where TProperty : struct
        {
            return value.HasValue ? setter(value.Value) : builder;
        }

        /// <summary>
        /// Call setter if specified value is not null.
        /// </summary>
        public static TBuilder SetIfNotNull<TBuilder, TProperty>(this TBuilder builder, TProperty value, Func<TProperty, TBuilder> setter)
            where TProperty : class
        {
            return value != null ? setter(value) : builder;
        }

        /// <summary>
        /// Call setter if specified value is not null.
        /// </summary>
        public static TBuilder SetIfNotNull<TBuilder, TProperty>(this TBuilder builder, TProperty? value, Func<TBuilder, Func<TProperty, TBuilder>> setterFactory)
            where TProperty : struct
        {
            return value.HasValue ? setterFactory(builder)(value.Value) : builder;
        }

        /// <summary>
        /// Call setter if specified value is not null.
        /// </summary>
        public static TBuilder SetIfNotNull<TBuilder, TProperty>(this TBuilder builder, TProperty value, Func<TBuilder, Func<TProperty, TBuilder>> setterFactory)
            where TProperty : class
        {
            return value != null ? setterFactory(builder)(value) : builder;
        }

        /// <summary>
        /// Call setter if specified condition is true.
        /// </summary>
        public static TBuilder SetIf<TBuilder>(this TBuilder builder, bool condition, Action<TBuilder> setter)
        {
            if (condition)
            {
                setter(builder);
            }

            return builder;
        }

        private static object mapValue(IBuilder builder, FieldDescriptor field, object currentValue, Func<FieldDescriptor, object, object> fieldValueMapper)
        {
            IMessage message = currentValue as IMessage;

            // If value is a message.
            if (message != null)
            {
                IBuilder innerMessageBuilder = builder
                    .CreateBuilderForField(field)
                    .MergeFrom(message, fieldValueMapper);

                return innerMessageBuilder.WeakBuild();
            }

            // If value is a primitive.
            return fieldValueMapper(field, currentValue);
        }
    }
}
