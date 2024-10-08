﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmGame.BeatButtons;
using RythmGame.Beats;
using System.Collections.Generic;

namespace RythmGame;

public class BeatGame : Microsoft.Xna.Framework.Game
{
    Texture2D arrowTexture;
    Texture2D arrow;
    Vector2 arrowPosition;
        
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static Vector2 origin = new Vector2(200, 200);

    // Create beat buttons for each direction
    BeatButtonUp upButton;
    BeatButtonDown downButton;
    BeatButtonLeft leftButton;
    BeatButtonRight rightButton;

        
    BeatManager beatManager;
    List<Beat> activeBeats = new List<Beat>();

    // Audio objects
    SoundEffect soundEffect;
    private SoundEffectInstance soundEffectInstance;


    public BeatGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        arrowPosition = new Vector2(75, 0);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Create a new SpriteBatch, which can be used to draw textures.
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        arrowTexture = Content.Load<Texture2D>("active-arrow");
        arrow = Content.Load<Texture2D>("full-arrow");

        soundEffect = Content.Load<SoundEffect>("GET PUMPING!!!");
        soundEffectInstance = soundEffect.CreateInstance();
        soundEffectInstance.IsLooped = true;

        // Create beat buttons for each direction
        upButton = new BeatButtonUp(arrowTexture);
        downButton = new BeatButtonDown(arrowTexture);
        leftButton = new BeatButtonLeft(arrowTexture);
        rightButton = new BeatButtonRight(arrowTexture);


        // Play the sound effect instance
        soundEffectInstance.Play();

        beatManager = new BeatManager(0.5f*0.38709677419f, origin, arrow, 300f);
        beatManager.LoadBeatsFromFile("../../../GET PUMPING!!!.txt");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardState keyboardState = Keyboard.GetState();

        arrowPosition.Y += 2;

        beatManager.Update(gameTime, activeBeats);

        upButton.Update(gameTime, keyboardState, activeBeats);
        downButton.Update(gameTime, keyboardState, activeBeats);
        leftButton.Update(gameTime, keyboardState, activeBeats);
        rightButton.Update(gameTime, keyboardState, activeBeats);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _graphics.GraphicsDevice.Clear(Color.Black);


        _spriteBatch.Begin();

        beatManager.Draw(_spriteBatch, activeBeats);

        // Draw all the beat buttons
        upButton.Draw(_spriteBatch);
        downButton.Draw(_spriteBatch);
        leftButton.Draw(_spriteBatch);
        rightButton.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}