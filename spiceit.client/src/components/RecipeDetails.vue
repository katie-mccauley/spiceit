<template>
  <Modal id="more-details">
    <template #title>
      <h2>
        {{ active.title }}
        <i
          title="delete ingredient"
          @click="deleteRecipe(active.id)"
          class="mdi mdi-delete text-danger pb-0 mb-0"
        ></i>
      </h2>
    </template>
    <template #body>
      <div class="container-fluid">
        <div class="row text-dark">
          <div class="col-md-3 p-0">
            <img
              :src="active.picture"
              class="w-100 object-fit-cover heightimg img-fluid h-100"
              alt=""
            />
          </div>
          <div class="col-md-4 m-2">
            <h2>The ingredients</h2>
            <div class="col-12 text-dark bg-g rounded shadow">
              <div class="row p-2">
                <div class="col-9">
                  <h5 v-for="i in ingredients" :key="i.id">
                    {{ i.quantity }}: {{ i.name }}
                  </h5>
                </div>
                <div class="col-2 mt-2">
                  <h6 v-for="i in ingredients" :key="i.id">
                    <i
                      class="mdi mdi-delete selectable"
                      @click="deleteIngredient(i.id)"
                    ></i>
                    <i
                      class="mdi mdi-pencil selectable"
                      data-bs-toggle="modal"
                      data-bs-target="#edit"
                      @click="activeIngred(i.id)"
                    ></i>
                  </h6>
                </div>
              </div>

              <CreateIngredient :idata="active" />
            </div>
          </div>
          <div class="col-md-4 m-2">
            <h2>The Steps</h2>
            <div class="col-12 text-dark bg-g rounded shadow">
              <div class="row p-2">
                <div class="col-9">
                  <h5 v-for="s in steps" :key="s.id">
                    {{ s.ordr }}: {{ s.body }}
                  </h5>
                </div>
                <div class="col-2 mt-2">
                  <h6 v-for="s in steps" :key="s.id">
                    <i
                      class="mdi mdi-delete selectable"
                      @click="deleteStep(s.id)"
                    ></i>
                    <i
                      class="mdi mdi-pencil selectable"
                      data-bs-toggle="modal"
                      data-bs-target="#edit-step"
                      @click="activeStep(s.id)"
                    ></i>
                  </h6>
                </div>
              </div>

              <CreateStep :sdata="active" />
            </div>
            <div class="row mt-5 justify-content-end me-2">
              <!-- <div
                class="col-md-2 me-4"
                v-if="
                  account.id == activeBoard.creatorId && route.name == 'Boards'
                "
              >
                <button
                  class="btn btn-danger text-bold"
                  title="delete saved keeped vaults"
                  @click="deletePin(active)"
                >
                  Delete Pin
                </button>
              </div> -->
              <div class="col-2 me-2">
                <button
                  class="btn outline-color dropdown-toggle"
                  data-bs-toggle="dropdown"
                  title="Add Keep to Vault"
                >
                  <h4><i class="mdi mdi-pin"></i></h4>
                </button>
                <ul
                  class="dropdown-menu right-col"
                  aria-labelledby="dropdownMenu2"
                >
                  <li v-for="p in myBoards" :key="p.id">
                    <button
                      class="dropdown-item"
                      type="button"
                      @click="createPin(p)"
                    >
                      <h5>{{ p.name }}</h5>
                    </button>
                  </li>
                </ul>
              </div>
              <div class="col-2">
                <img
                  :src="active.creator?.picture"
                  class="img-fluid cropped selectable"
                  alt=""
                  @click="goToProfile(active.creatorId)"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { Modal } from "bootstrap"
import { ingredientsService } from "../services/IngredientsService"
import { recipesService } from "../services/RecipesService"
import { stepsService } from "../services/StepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { pinsService } from "../services/PinsService"
import { useRoute, useRouter } from "vue-router"
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    return {
      route,
      async deleteIngredient(id) {
        try {
          if (await Pop.confirm()) {
            await ingredientsService.deleteIngredient(id)
          }
        } catch (error) {
          logger.error(error)
        }
      },
      async deleteStep(id) {
        try {
          if (await Pop.confirm()) {
            await stepsService.deleteStep(id)
          }
        } catch (error) {
          logger.error(error)
        }
      },
      async getAll() {
        try {
          await recipesService.getAllRecipes()
        } catch (error) {
          logger.error(error)
        }
      },
      async activeIngred(id) {
        try {
          await ingredientsService.getOne(id)
        } catch (error) {
          logger.error(error)
        }
      },
      async getMine() {
        try {
          await recipesService.getMine()
        } catch (error) {
          logger.error(error)

        }
      },
      async activeStep(id) {
        try {
          await stepsService.getOne(id)
        } catch (error) {
          logger.error(error)
        }
      },
      async deleteRecipe(id) {
        try {
          if (await Pop.confirm()) {
            await recipesService.deleteRecipe(id)
            Modal.getOrCreateInstance(document.getElementById('more-details')).hide()
          }

        } catch (error) {
          logger.error
        }
      },
      async goToProfile(id) {
        try {
          Modal.getOrCreateInstance(document.getElementById('more-details')).hide()
          router.push({ name: 'Profile', params: { id } })
        } catch (error) {
          logger.error(error)
        }
      },
      async createPin(pin) {
        try {
          const pinData = {
            boardId: pin.id,
            recipeId: AppState.activeRecipe.id
          }
          await pinsService.createPin(pinData)
        } catch (error) {
          logger.error(error)
        }
      },
      async deletePin(pin) {
        try {
          logger.log(pin, "pinnn")
        } catch (error) {
          logger.error(error)
        }
      },
      ingredients: computed(() => AppState.ingredients),
      steps: computed(() => AppState.steps),
      active: computed(() => AppState.activeRecipe),
      activei: computed(() => AppState.activeIngredient),
      actives: computed(() => AppState.activeStep),
      myBoards: computed(() => AppState.accountBoards),
      activeBoard: computed(() => AppState.activeBoard),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.bg-g {
  background-color: #e9eaec;
}

.cropped {
  height: 50px;
  max-width: 50px;
  border-radius: 50%;
}

.greentext {
  color: rgba(33, 150, 83, 1);
}

.navbtn-y {
  transform: translateY(-40%);
}

.heightimg {
  height: 400px;
}

.object-fit-cover {
  object-fit: cover;
}
</style>