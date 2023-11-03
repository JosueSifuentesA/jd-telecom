﻿// <auto-generated />
using System;
using JDTelecomunicaciones.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JDTelecomunicaciones.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231102040812_EightMigration")]
    partial class EightMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JDTelecomunicaciones.Models.MensajeContacto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("AutorizoPublicidad")
                        .HasColumnType("boolean")
                        .HasColumnName("autorizo_publicidad");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("correo_electronico");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("dni");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nombre_completo");

                    b.Property<string>("NumeroTelefono")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("numero_telefono");

                    b.Property<bool>("PoliticasPrivacidad")
                        .HasColumnType("boolean")
                        .HasColumnName("politicas_privacidad");

                    b.HasKey("Id");

                    b.ToTable("mensaje_contacto");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Persona", b =>
                {
                    b.Property<int>("id_persona")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_persona");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id_persona"));

                    b.Property<string>("apMatPersona")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("apPatPersona")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("dniPersona")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nombrePersona")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<char>("sexoPersona")
                        .HasColumnType("character(1)");

                    b.HasKey("id_persona");

                    b.ToTable("persona");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Planes", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_plan");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("descripcion_plan")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nombre_plan")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("precio_plan")
                        .HasColumnType("numeric");

                    b.Property<int>("velocidad_plan")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("planes");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Promocion", b =>
                {
                    b.Property<int>("id_promocion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_promocion");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id_promocion"));

                    b.Property<string>("efecto_promocion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("imgSubidaByte")
                        .HasColumnType("bytea");

                    b.Property<string>("nombre_promocion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("usuarioid_usuario")
                        .HasColumnType("integer");

                    b.HasKey("id_promocion");

                    b.HasIndex("usuarioid_usuario");

                    b.ToTable("promocion");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Recibos", b =>
                {
                    b.Property<int>("idRecibo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_recibo");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idRecibo"));

                    b.Property<string>("estado_recibo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("fecha_vencimiento")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("mes_recibo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("monto_recibo")
                        .HasColumnType("numeric");

                    b.Property<string>("plan_recibo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("usuarioid_usuario")
                        .HasColumnType("integer");

                    b.HasKey("idRecibo");

                    b.HasIndex("usuarioid_usuario");

                    b.ToTable("recibo");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Servicios", b =>
                {
                    b.Property<int>("Id_servicios")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_servicios");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id_servicios"));

                    b.Property<char>("Estado_Servicio")
                        .HasColumnType("character(1)");

                    b.Property<string>("FechaActivacion_Servicio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PeriodoFacturacion_Servicio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Plan_Servicioid")
                        .HasColumnType("integer");

                    b.HasKey("Id_servicios");

                    b.HasIndex("Plan_Servicioid");

                    b.ToTable("servicios");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Tickets", b =>
                {
                    b.Property<int>("id_ticket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_ticket");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id_ticket"));

                    b.Property<string>("descripcion_ticket")
                        .HasColumnType("text");

                    b.Property<string>("fecha_ticket")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("status_ticket")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("tipoProblematica_ticket")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("usuarioid_usuario")
                        .HasColumnType("integer");

                    b.HasKey("id_ticket");

                    b.HasIndex("usuarioid_usuario");

                    b.ToTable("ticket");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Usuario", b =>
                {
                    b.Property<int>("id_usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_usuario");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id_usuario"));

                    b.Property<string>("contraseña_usuario")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("correo_usuario")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nombre_usuario")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("personaid_persona")
                        .HasColumnType("integer");

                    b.Property<char>("rol_usuario")
                        .HasColumnType("character(1)");

                    b.Property<int?>("serviciosId_servicios")
                        .HasColumnType("integer");

                    b.HasKey("id_usuario");

                    b.HasIndex("personaid_persona");

                    b.HasIndex("serviciosId_servicios");

                    b.ToTable("usuario");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Promocion", b =>
                {
                    b.HasOne("JDTelecomunicaciones.Models.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioid_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Recibos", b =>
                {
                    b.HasOne("JDTelecomunicaciones.Models.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioid_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Servicios", b =>
                {
                    b.HasOne("JDTelecomunicaciones.Models.Planes", "Plan_Servicio")
                        .WithMany()
                        .HasForeignKey("Plan_Servicioid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan_Servicio");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Tickets", b =>
                {
                    b.HasOne("JDTelecomunicaciones.Models.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioid_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("JDTelecomunicaciones.Models.Usuario", b =>
                {
                    b.HasOne("JDTelecomunicaciones.Models.Persona", "persona")
                        .WithMany()
                        .HasForeignKey("personaid_persona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JDTelecomunicaciones.Models.Servicios", "servicios")
                        .WithMany()
                        .HasForeignKey("serviciosId_servicios");

                    b.Navigation("persona");

                    b.Navigation("servicios");
                });
#pragma warning restore 612, 618
        }
    }
}
