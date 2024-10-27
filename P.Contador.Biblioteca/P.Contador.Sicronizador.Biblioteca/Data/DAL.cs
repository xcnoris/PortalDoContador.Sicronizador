

using Integrador_Com_CRM.Metodos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Integrador_Com_CRM.Data
{
    internal class DAL<T> : IDisposable where T : class
    {
        private readonly IntegradorDBContext context;

        public DAL(IntegradorDBContext context)
        {
            this.context = context;
        }
        // Implementação do método Dispose
        public void Dispose()
        {
            context?.Dispose(); // Libera o contexto do banco de dados
        }

        public async Task<IEnumerable<T>> ListarAsync()
        {
            try
            {
                return await context.Set<T>().ToListAsync();

            }
            catch (Exception Exception)
            {
                MetodosGerais.RegistrarLog("Conexao", Exception.Message);
                throw new Exception(Exception.Message);
            }
        }

        public async Task AdicionarAsync(T objeto)
        {
            try
            {
                await context.Set<T>().AddAsync(objeto);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                MetodosGerais.RegistrarLog("Conexao", ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task AtualizarAsync(T objeto)
        {
            try
            {
                context.Set<T>().Update(objeto);
                await context.SaveChangesAsync();
            }
            catch (Exception Exception)
            {
                MetodosGerais.RegistrarLog("Conexao", Exception.Message);
                throw new Exception(Exception.Message);
            }
        }


        public async Task DeletarAsync(T objeto)
        {
            try
            {
                context.Set<T>().Remove(objeto);
                await context.SaveChangesAsync();
            }
            catch (Exception Exception)
            {
                MetodosGerais.RegistrarLog("Conexao", Exception.Message);
                throw new Exception(Exception.Message);
            }
        }

        public async Task DeletarPorCondicaoAsync(Expression<Func<T, bool>> filtro)
        {
            try
            {
                var objetos = context.Set<T>().Where(filtro).ToList();
                if (objetos.Any())
                {
                    context.Set<T>().RemoveRange(objetos);
                    await context.SaveChangesAsync();
                }
                else
                {
                    MetodosGerais.RegistrarLog("Conexao", "Nenhum objeto encontrado para deletar.");
                }
            }
            catch (Exception Exception)
            {
                MetodosGerais.RegistrarLog("Conexao", Exception.Message);
                throw new Exception(Exception.Message);
            }
        }

        public T BuscarPor(Func<T, bool> condicao)
        {
            try
            {
                return context.Set<T>().FirstOrDefault(condicao);
            }
            catch (Exception Exception)
            {
                MetodosGerais.RegistrarLog("Conexao", Exception.Message);
                throw new Exception(Exception.Message);
            }
        }

        public async Task<T> BuscarPorAsync(Expression<Func<T, bool>> condicao)
        {
            try
            {
                return await context.Set<T>().FirstOrDefaultAsync(condicao);
            }
            catch (Exception Exception)
            {
                MetodosGerais.RegistrarLog("Conexao", Exception.Message);
                throw new Exception(Exception.Message);
            }
        }

        public async Task<T> RecuperarPorAsync(Expression<Func<T, bool>> condicao, string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = context.Set<T>();

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                return await query.FirstOrDefaultAsync(condicao);
            }
            catch (Exception Exception)
            {
                MetodosGerais.RegistrarLog("Conexao", Exception.Message);
                throw new Exception(Exception.Message);
            }
        }

        public async Task<IEnumerable<T>> RecuperarTodosPorAsync(Expression<Func<T, bool>> condicao, string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = context.Set<T>();

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                return await query.Where(condicao).ToListAsync();
            }
            catch (Exception Exception)
            {
                MetodosGerais.RegistrarLog("Conexao", Exception.Message);
                return Enumerable.Empty<T>();
                throw new Exception(Exception.Message);
            }
        }
    }
}
