﻿using Whoever.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

namespace Whoever.Web.Extensions
{
    /// <summary>
    /// <see cref="IApplicationBuilder"/> extension methods.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally add to the request execution pipeline.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the request execution pipeline.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseIf(
            this IApplicationBuilder application,
            bool condition,
            Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (condition)
            {
                application = action(application);
            }

            return application;
        }

        /// <summary>
        /// Executes the specified <paramref name="ifAction"/> if the specified <paramref name="condition"/> is
        /// <c>true</c>, otherwise executes the <paramref name="elseAction"/>. This can be used to conditionally add to
        /// the request execution pipeline.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to add to the request execution pipeline if the condition is
        /// <c>true</c>.</param>
        /// <param name="elseAction">The action used to add to the request execution pipeline if the condition is
        /// <c>false</c>.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseIfElse(
            this IApplicationBuilder application,
            bool condition,
            Func<IApplicationBuilder, IApplicationBuilder> ifAction,
            Func<IApplicationBuilder, IApplicationBuilder> elseAction)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (ifAction == null)
            {
                throw new ArgumentNullException(nameof(ifAction));
            }

            if (elseAction == null)
            {
                throw new ArgumentNullException(nameof(elseAction));
            }

            if (condition)
            {
                application = ifAction(application);
            }
            else
            {
                application = elseAction(application);
            }

            return application;
        }

        /// <summary>
        /// Executes the specified action using the <see cref="HttpContext"/> to determine if the specified
        /// <paramref name="condition"/> is <c>true</c> which can be used to conditionally add to the request execution
        /// pipeline.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the request execution pipeline.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseIf(
            this IApplicationBuilder application,
            Func<HttpContext, bool> condition,
            Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var builder = application.New();

            action(builder);

            return application.Use(next =>
            {
                builder.Run(next);

                var branch = builder.Build();

                return context =>
                {
                    if (condition(context))
                    {
                        return branch(context);
                    }

                    return next(context);
                };
            });
        }

        /// <summary>
        /// Executes the specified <paramref name="ifAction"/> using the <see cref="HttpContext"/> to determine if the
        /// specified <paramref name="condition"/> is <c>true</c>, otherwise executes the
        /// <paramref name="elseAction"/>. This can be used to conditionally add to the request execution pipeline.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to add to the request execution pipeline if the condition is
        /// <c>true</c>.</param>
        /// <param name="elseAction">The action used to add to the request execution pipeline if the condition is
        /// <c>false</c>.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseIfElse(
            this IApplicationBuilder application,
            Func<HttpContext, bool> condition,
            Func<IApplicationBuilder, IApplicationBuilder> ifAction,
            Func<IApplicationBuilder, IApplicationBuilder> elseAction)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (ifAction == null)
            {
                throw new ArgumentNullException(nameof(ifAction));
            }

            if (elseAction == null)
            {
                throw new ArgumentNullException(nameof(elseAction));
            }

            var ifBuilder = application.New();
            var elseBuilder = application.New();

            ifAction(ifBuilder);
            elseAction(elseBuilder);

            return application.Use(next =>
            {
                ifBuilder.Run(next);
                elseBuilder.Run(next);

                var ifBranch = ifBuilder.Build();
                var elseBranch = elseBuilder.Build();

                return context =>
                {
                    if (condition(context))
                    {
                        return ifBranch(context);
                    }
                    else
                    {
                        return elseBranch(context);
                    }
                };
            });
        }

        /// <summary>
        /// Returns a 500 Internal Server Error response when an unhandled exception occurs.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseInternalServerErrorOnException(this IApplicationBuilder application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            return application.UseMiddleware<InternalServerErrorOnExceptionMiddleware>();
        }

        /// <summary>
        /// Removes the Server HTTP header from the HTTP response for marginally better security and performance.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseNoServerHttpHeader(this IApplicationBuilder application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            return application.UseMiddleware<NoServerHttpHeaderMiddleware>();
        }

        /// <summary>
        /// Adds developer friendly error pages for the application which contain extra debug and exception information.
        /// Note: It is unsafe to use this in production.
        /// </summary>
        public static IApplicationBuilder UseDeveloperErrorPages(this IApplicationBuilder application) =>
            application
                // When a database error occurs, displays a detailed error page with full diagnostic information. It is
                // unsafe to use this in production. Uncomment this if using a database.
                // .UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
                // When an error occurs, displays a detailed error page with full diagnostic information.
                // See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
                .UseDeveloperExceptionPage();

        /// <summary>
        /// Adds user friendly error pages.
        /// </summary>
        public static IApplicationBuilder UseErrorPages(this IApplicationBuilder application)
        {
            // Add error handling middle-ware which handles all HTTP status codes from 400 to 599 by re-executing
            // the request pipeline for the following URL. '{0}' is the HTTP status code e.g. 404.
            application.UseStatusCodePagesWithReExecute("/error/{0}/");

            // Returns a 500 Internal Server Error response when an unhandled exception occurs.
            application.UseInternalServerErrorOnException();

            return application;
        }

       
    }
}
