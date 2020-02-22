/*
Copyright (c) 2020 Jeff Campbell

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace JCMG.COC.Editor
{
	public static class ReflectionUtility
	{
		/// <summary>
		///     Returns an IEnumerable of class instances of Types derived from T that are not abstract or generic
		///     and have a default, empty constructor.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static IEnumerable<T> GetAllDerivedInstancesOfType<T>() where T : class
		{
			var objects = new List<T>();
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (var assembly in assemblies)
			foreach (var type in assembly.GetTypes()
				.Where(myType => myType.IsClass &&
				                 !myType.IsAbstract &&
				                 !myType.IsGenericType &&
				                 myType.IsSubclassOf(typeof(T)) &&
				                 myType.GetConstructor(Type.EmptyTypes) != null))
				objects.Add((T) Activator.CreateInstance(type));

			return objects;
		}
	}
}
