﻿using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sandbox.Domain;
using Swashbuckle.AspNetCore.Swagger;

namespace Sandbox.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<SandboxDbContext>(opt => 
				opt.UseInMemoryDatabase("Sandbox"));
			
			services.AddMvc()
				.AddControllersAsServices() // Allows Autofac to manage DI for controllers
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Sandbox Api", Version = "v1" });
			});

			var builder = new ContainerBuilder();
			
			builder.Populate(services);

			builder.RegisterType<SandboxDbContext>()
				.As<IUnitOfWork>()
				.As<IQueryEntities>()
				.As<IDbContext>()
				.InstancePerLifetimeScope();

			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			
			// Register IQueryHandlers and decorate them
			builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IQueryHandler<,>), "queryHandler").InstancePerLifetimeScope();
			builder.RegisterGenericDecorator(typeof(ValidationQueryHandlerDecorator<,>), typeof(IQueryHandler<,>), "queryHandler");

			// Register ICommandHandlers and decorate them
			builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(ICommandHandler<>), "commandHandler").InstancePerLifetimeScope();
			builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<>), typeof(ICommandHandler<>), "commandHandler", "validatorCommandHandler");
			builder.RegisterGenericDecorator(typeof(DeadlockRetryCommandHandlerDecorator<>), typeof(ICommandHandler<>), "validatorCommandHandler");

			return new AutofacServiceProvider(builder.Build());
		}
		
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHttpsRedirection();
				app.UseHsts();
			}

			app.UseSwagger();

			app.UseSwaggerUI(c => 
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sandbox Api");
				c.RoutePrefix = string.Empty; // Serve swagger at root of website
			});

			app.UseMvc();
		}
	}
}
