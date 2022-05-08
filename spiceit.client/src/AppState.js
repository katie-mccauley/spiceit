import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  profile: {},
  recipes: [],
  ingredients: [],
  steps: [],
  activeRecipe: {},
  favs: [],
  activeIngredient: {},
  activeStep: {},
  favorites: [],
  mine: [],
  boardsUser: [],
  recipesUser: [],
  activeBoard: {},
  pins: []
})
