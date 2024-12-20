VAR pokemon = "... never mind"
Welcom wanderer ! #speaker: professor
-> main

=== main ===
Which pokemon do you choose ?
    * Charmander ? [] You did !#speaker: professor
        ~ pokemon = "Charmander"
        You Chose {pokemon} !#speaker: friend
        Are you sure you want {pokemon} ?
            ** [Yes]#speaker: you #image: charmander_image
                Okay finally !#speaker: friend 
                -> chosen("Charmander")
            **[No, i want to change]#speaker: you
                pffff...#speaker: friend
                -> main
    * [Bulbasaur ?] Great choice !#speaker: professor
        You Chose Bulbasaur !#speaker: friend
        Are you sur tho ?#speaker: professor
            ** Yes, i am#speaker: you #image: bulbasaur_image
                -> chosen("Bulbausar")
            **No, i want to change#speaker: you
                -> main
    * Squirtle ? [] You did !#speaker: professor #image: squirtle_image
        You Chose Squirtle !#speaker: friend
        -> main
    * ->
    There are no more pokemons left to choose.#speaker: professor
->END
=== chosen (chosen_pokemon) ===
You chose {chosen_pokemon} ! But i prefer {pokemon}#speaker: friend
- that conversation was great !#speaker: professor
-> END