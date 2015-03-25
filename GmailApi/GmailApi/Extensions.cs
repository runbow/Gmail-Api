﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleApplication1
{
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="replaceWith"></param>
        /// <returns></returns>
        public static String GetValidFilename(this String name, char replaceWith = '_')
        {
            if (Path.GetInvalidFileNameChars().Contains(replaceWith))
                throw new Exception(String.Concat("Replacement char '", replaceWith, "' is not valid!"));
            if (name.Length > 256)// total file including path max is 256 chars
                name = new string(name.Take(260).ToArray());

            return new String(name.Select(s => Path.GetInvalidFileNameChars().Contains(s) ? replaceWith : s).ToArray());
        }

        public static T GetAttribute<T, T2>(this T2 value)
            where T : Attribute
            where T2 : struct , IConvertible// enum
        {
            Type type = typeof(T2);
            string name = Enum.GetName(type, value);

            return type.GetField(name).GetCustomAttribute<T>();
        }

        /// <summary>
        /// Get the integer values of an Enum which uses [Flags]
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static T[] GetFlagEnumValues<T>(this T e) where T : struct, IConvertible // = enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<int>()
                .Where(s => s > 0)// Flag enum should start with 1 (2,4,etc)
                .Where(f => (f & Convert.ToInt32(e)) == f)
                .Cast<T>()
                .ToArray();
        }
    }
}