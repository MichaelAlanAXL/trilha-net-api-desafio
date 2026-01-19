using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using Microsoft.AspNetCore.Authorization;

namespace TrilhaApiDesafio.Controllers
{
    /// <summary>
    /// Controller responsável pelas operações relacionadas às tarefas
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;


        /// <summary>
        /// Construtor do TarefaController
        /// </summary>
        /// <param name="context"></param>
        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma tarefa pelo Id
        /// </summary>
        /// <param name="id">Identificador único da tarefa</param>
        /// <returns>Tarefa encontrada</returns>
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            var tarefa = _context.Tarefas.Find(id);
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            if (tarefa == null && tarefa.Id != id)
            {
                return NotFound();
            }
            // caso contrário retornar OK com a tarefa encontrada
            return Ok(tarefa);
        }

        /// <summary>
        /// Obtém todas as tarefas
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var tarefa = _context.Tarefas.ToList();
            return Ok(tarefa);
        }

        /// <summary>
        /// Obtém tarefas por título
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            // Dica: Usar como exemplo o endpoint ObterPorData
            return Ok(tarefa);
        }

        /// <summary>
        /// Obtém tarefas por data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        /// <summary>
        /// Obtém tarefas filtradas por status
        /// </summary>
        /// <remarks>
        /// <b>Status aceitos:</b>
        /// <br/>Pendente
        /// <br/>EmAndamento
        /// <br/>Finalizado
        /// <br/>Cancelado
        /// <br/>Atrasado
        /// </remarks>
        /// <param name="status">Status da tarefa</param>
        /// <returns>Lista de tarefas</returns>
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            // Dica: Usar como exemplo o endpoint ObterPorData
            return Ok(tarefa);
        }

        /// <summary>
        /// Cria uma nova tarefa
        /// </summary>
        /// <remarks>
        /// <b>Exemplo de requisição:</b>
        /// <pre>
        /// <code>
        /// POST /api/terefas
        /// {
        ///     "titulo": "Estudar para a prova", <br/>
        ///     "descricao": "Estudar os capítulos 4, 5 e 6 do livro", <br/>
        ///     "status": "Pendente" <br/>
        ///  }
        /// </code>
        /// </pre>
        /// <b>Status possíveis:</b>
        /// <br/>- Pendente
        /// <br/>- EmAndamento
        /// <br/>- Finalizado
        /// <br/>- Cancelado
        /// <br/>- Atrasado
        /// </remarks>
        /// <returns>Tarefa criada</returns>
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        /// <summary>
        /// Atualiza uma tarefa existente
        /// </summary>
        /// <remarks>
        /// <b>Campos que podem ser alterados:</b>
        /// <br/>- Título
        /// <br/>- Descrição
        /// <br/>- Status
        /// </remarks>
        /// <param name="id">ID da tarefa</param>
        /// <param name="tarefa">Dados da tarefa a ser atualizada</param>
        /// <returns>Tarefa atualizada</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Deleta/remove uma tarefa pelo ID
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        /// <returns>Tarefa deletada</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
