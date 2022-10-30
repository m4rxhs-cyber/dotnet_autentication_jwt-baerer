using Microsoft.AspNetCore.Mvc;
using ProjetoMySQL.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 
namespace ProjetoMySQL.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EleitorController : ControllerBase
    {
        private BDContexto contexto;
 
        public EleitorController(BDContexto bdContexto)
        {
            contexto = bdContexto;
        }
 
        [HttpGet]
        [Authorize(Roles = "candidato")]
        public List<Eleitor> Listar()
        {
            return contexto.Eleitores.ToList();
        }

        [HttpPost]
        public string Cadastrar([FromBody] Eleitor novoEleitor)
        {
            contexto.Add(novoEleitor);
            contexto.SaveChanges();
            return "Eleitor(a) cadastrado(a) com sucesso!";
        }

        [HttpDelete]
        public string Excluir([FromBody] int id)
        {
            Eleitor dados = contexto.Eleitores.FirstOrDefault(p => p.Id == id);

            if (dados == null)
            {
                return "Não foi encontrado Eleitor para o ID informado";
            }
            else
            {
                contexto.Remove(dados);
                contexto.SaveChanges();

                return "Eleitor(a) removido(a) com sucesso!";
            }
        }


        [HttpPut]
        public string Alterar([FromBody] Eleitor eleitorAtualizado)
        {
            contexto.Update(eleitorAtualizado);
            contexto.SaveChanges();

            return "Eleitor atualizado com sucesso!";
        }


        [HttpGet] 
        public Eleitor Visualizar(int id)
        {
            return contexto.Eleitores.FirstOrDefault(p => p.Id == id);
        }


        [HttpGet] 
        public Eleitor BuscaPorCpf(string cpf)
        {
            return contexto.Eleitores.FirstOrDefault(p => p.Cpf == cpf);
        }
    }
}