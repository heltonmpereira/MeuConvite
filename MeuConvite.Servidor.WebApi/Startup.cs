using System;
using MeuConvite.Definicao.Entidade;
using MeuConvite.Modelo.Contexto;
using MeuConvite.Modelo.Persistencia;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MeuConvite.Servidor.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var dbCtx = new MeuConviteContexto(new DbContextOptionsBuilder().UseInMemoryDatabase("MeuConviteDb").Options);
            //services.AddDbContext<MeuConviteContext>();
            services.AddScoped(provider => new UsuarioDao(dbCtx));
            services.AddScoped(provider => new ContatoDao(dbCtx));

            var configuracoesAssinatura = new ConfiguracoesAssinatura();
            var configuracoesToken = new ConfiguracoesToken();
            new ConfigureFromConfigurationOptions<ConfiguracoesToken>(
                    Configuration.GetSection("ConfiguracoesToken")
                ).Configure(configuracoesToken);

            services.AddSingleton(configuracoesAssinatura);
            services.AddSingleton(configuracoesToken);

            services
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearerOptions =>
                {
                    var paramsValidation = bearerOptions.TokenValidationParameters;
                    paramsValidation.IssuerSigningKey = configuracoesAssinatura.Chave;
                    paramsValidation.ValidAudience = configuracoesToken.Espectador;
                    paramsValidation.ValidIssuer = configuracoesToken.Emissor;

                    //Validar a assinatura do token recebido
                    paramsValidation.ValidateIssuerSigningKey = true;

                    //Verificar se o token ainda é válido
                    paramsValidation.ValidateLifetime = true;

                    // Tempo de tolerância para a expiração de um token (utilizado
                    // caso haja problemas de sincronismo de horário entre diferentes
                    // computadores envolvidos no processo de comunicação)
                    paramsValidation.ClockSkew = TimeSpan.Zero;
                });

            //Ativar o token para os recursos do projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer",
                    new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            [FromServices] UsuarioDao daoUsuario, 
            [FromServices] ContatoDao daoContato)
        {
            InicializarDb(daoUsuario, daoContato);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void InicializarDb(UsuarioDao daoUsuario, ContatoDao daoContato)
        {
            daoUsuario.Incluir(new Usuario { Id = "admin", Senha = "admin" });

            daoContato.Incluir(new Contato { Nome = "Vinicius Azevedo Cavalcanti", Email = "viniciusazevedocavalcanti@dayrep.com", Celular = "(31) 2014-5559" });
            daoContato.Incluir(new Contato { Nome = "Beatriz Azevedo Cardoso", Email = "beatrizazevedocardoso@rhyta.com", Celular = "(98) 7223-7829" });
        }
    }
}
