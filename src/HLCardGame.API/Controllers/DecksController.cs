using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HLCardGame.API.Results;
using HLCardGame.Application;
using HLCardGame.Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;

namespace HLCardGame.API.Controllers
{
    /// <summary>
    /// this controller is resposible for managing the deck cards game
    /// it do the following
    /// 1- create deck with suffeled cards
    /// 2- withdraw card
    /// 3- guess next card
    /// 4- cancel the game
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("[controller]")]
    [ApiController]
    public class DecksController : ControllerBase
    {
        private IDeckService _gameControlService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DecksController"/> class.
        /// </summary>
        /// <param name="gameControlService">The game control service.</param>
        /// <exception cref="System.ArgumentNullException">gameControlService</exception>
        public DecksController(IDeckService gameControlService)
        {
            _gameControlService = gameControlService
                ?? throw new ArgumentNullException(nameof(gameControlService));
        }

        /// <summary>
        /// Creates the new deck with shuffeld 52 cards .
        /// </summary>
        /// <param name="numberOfPlayers">The number of players.</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(DeckResults), (int)HttpStatusCode.OK)]
        [HttpPost("new/shuffle/{numberOfPlayers}")]
        public async Task<IActionResult> CreateNewDeck(int numberOfPlayers)
        {
            if (numberOfPlayers <= 0)
                return BadRequest("number of player should be greater than 0");

            var result = await _gameControlService.CreateDeckAsync(numberOfPlayers);

            return Ok(new DeckResults(
                deckId: result.DeckId,
                shuffled: true,
                playerTurn: result.PlayerTurn,
                nPlayer: result.NPlayers,
                remaining: result.Cards.Count()));
        }

        /// <summary>
        /// Gets the deck info by identifier.
        /// </summary>
        /// <param name="deckId">The deck identifier.</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(DeckResults), (int)HttpStatusCode.OK)]
        [HttpGet("{deckId}")]
        public async Task<IActionResult> GetDeckById(Guid deckId)
        {
            var result = await _gameControlService.GetDeckIdByIdAsync(deckId);
            if (result is null)
                return NotFound();

            return Ok(new DeckResults(
                deckId: result.DeckId,
                shuffled: true,
                playerTurn: result.PlayerTurn,
                nPlayer: result.NPlayers,
                remaining: result.Cards.Count()));
        }

        /// <summary>
        /// Draws the card from the deck if possible.
        /// </summary>
        /// <param name="deckId">The deck identifier.</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CardResults), (int)HttpStatusCode.OK)]
        [HttpGet("{deckId}/DrawCard")]
        public async Task<IActionResult> DrawCard(Guid deckId)
        {
            var result = await _gameControlService.DrawCardAsync(deckId);

            return Ok(new CardResults(
                   color: result.Color,
                   suit: result.Suit,
                   value: result.Value,
                   displayName: result.DisplayName));
        }

        /// <summary>
        /// Guesses the next card.
        /// </summary>
        /// <param name="deckId">The deck identifier.</param>
        /// <param name="guessDirection">The guess direction 0 for higher 1 for lower.</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [HttpGet("{deckId}/GuessNextIs/{guessDirection}")]
        public async Task<IActionResult> GuessNextCard(Guid deckId, GuessDirection guessDirection)
        {
            var result = await _gameControlService.GuessNextCardAsync(deckId, guessDirection);

            return Ok(new GuessResults(
                sucess: result.Item1,
                deckCard: new CardResults(
                   color: result.Item2.Color,
                   suit: result.Item2.Suit,
                   value: result.Item2.Value,
                   displayName: result.Item2.DisplayName),
                card: new CardResults(
                   color: result.Item3.Color,
                   suit: result.Item3.Suit,
                   value: result.Item3.Value,
                   displayName: result.Item3.DisplayName)));
        }

        /// <summary>
        /// Deletes the Deck.
        /// </summary>
        /// <param name="deckId">The Deck identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> CancelDeck(Guid deckId)
        {
            var result = await _gameControlService.CancelDeckAsync(deckId);
            if (result == 1)
                return Ok();
            else
                return NotFound();
        }
    }
}