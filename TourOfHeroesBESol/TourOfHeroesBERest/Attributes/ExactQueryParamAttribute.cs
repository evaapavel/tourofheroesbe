using Microsoft.AspNetCore.Mvc.ActionConstraints;

using System;
using System.Collections.Generic;
using System.Linq;



namespace TourOfHeroesBERest.Attributes
{



    /// <summary>
    /// Helps to differentiate between almost same actions that differ in their query strings only.
    /// </summary>
    //[AttributeUsage(AttributeTargets.Method)]
    public class ExactQueryParamAttribute : Attribute, IActionConstraint
    {



        private readonly string[] keys;



        public ExactQueryParamAttribute(params string[] keys)
        {
            this.keys = keys;
        }



        public int Order => 0;



        public bool Accept(ActionConstraintContext context)
        {
            var query = context.RouteContext.HttpContext.Request.Query;
            return (query.Count == keys.Length) && (keys.All(key => query.ContainsKey(key)));
        }



    }



}
