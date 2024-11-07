﻿using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Mappings;
using ApplicationCore.Wrappers;
using Domain.Entities;
using Finbuckle.MultiTenant;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestructure.Persistance.ColaboradorService
{
    public class ColaboradorService : IColaborardorService
    {
        private readonly ApplicationDbContext _context;
        public ColaboradorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<object>> CreateColaborador(Colaboradores colaboradores)
        {
            Response<object> response = new();

            try
            {
                var results = await _context.Colaboradores.AddAsync(colaboradores);
                await _context.SaveChangesAsync();

                if (colaboradores.IsProfesor)
                {
                    Profesor profesor = new Profesor
                    {
                        FkColaborador = colaboradores.Id,
                        Correo = $"{colaboradores.Nombre.ToLower()}@example.com",
                        Departamento = "string",
                    };

                    await _context.Profesor.AddAsync(profesor);
                }
                else
                {
                    Administrativo administrativo = new Administrativo
                    {
                        FkColaborador = colaboradores.Id,
                        Correo = $"{colaboradores.Nombre.ToLower()}@example.com",
                        Puesto = "string",
                        Nomina  = 123456
                    };

                    await _context.Administrativo.AddAsync(administrativo);
                }

                await _context.SaveChangesAsync();

                response.Result = colaboradores;
                response.Message = "Colaborador creado correctamente";
                response.Succeeded = true;
                return response;

            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Result = ex.Message;
                return response;
            }
        }

        public async Task<Response<List<Colaboradores>>> GetColaboradoresPorFecha(DateTime FechaInicio, DateTime FechaFin)
        {
            Response<List<Colaboradores>> response = new();

            try
            {
                // Filtramos los colaboradores que están dentro del rango de fechas
                var results = await _context.Colaboradores
                    .Where(c => c.FechaInicio >= FechaInicio && c.FechaFin <= FechaFin)
                    .ToListAsync();

                // Si no se encontraron colaboradores, podemos mandar un mensaje adecuado.
                if (results == null || results.Count == 0)
                {
                    response.Succeeded = false;
                    response.Message = "No se encontraron colaboradores para el rango de fechas especificado.";
                    response.Result = null;
                }
                else
                {
                    response.Succeeded = true;
                    response.Message = "Lista de colaboradores obtenida correctamente.";
                    response.Result = results;
                }

                return response;
            }
            catch (Exception ex)
            {
                // En caso de error, capturamos la excepción y devolvemos un mensaje adecuado
                response.Succeeded = false;
                response.Message = "Ocurrió un error al obtener los colaboradores: " + ex.Message;
                response.Result = null;

                return response;
            }
        }



    }
}