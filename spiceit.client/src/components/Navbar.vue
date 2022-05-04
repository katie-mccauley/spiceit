<template>
  <nav
    class="
      navbar navbar-expand-lg navbar-dark
      bg-dark
      px-3
      bg-image
      align-items-start
    "
  >
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column">
        <!-- <img alt="logo" src="../assets/img/cw-logo.png" height="45" /> -->
        <h1>SpiceIt</h1>
      </div>
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="col-md-6 justify-content-end align-items-end mt-2 ms-5 me-auto">
      <form @submit.prevent="search">
        <i class="fa fa-search"></i>
        <input
          type="text"
          class="form-control form-input"
          placeholder="Search anything..."
          v-model="query"
        />
      </form>
    </div>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <!-- <li>
          <router-link
            :to="{ name: 'About' }"
            class="btn text-success lighten-30 selectable text-uppercase"
          >
            About
          </router-link>
        </li> -->
        <li>
          <!-- <h1>
            <i
              data-bs-toggle="modal"
              data-bs-target="#create-recipe"
              class="mdi mdi-plus selectable"
            ></i>
          </h1> -->
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { computed, ref } from "@vue/reactivity";
import { AppState } from "../AppState";
import { recipesService } from "../services/RecipesService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
export default {
  setup() {
    const query = ref("")
    return {

      recipe: computed(() => AppState.recipes),
      query,
      async search() {
        try {
          await recipesService.getAllRecipes('?search=' + query.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    };
  },
};
</script>

<style scoped>
* {
  font-family: Sahitya;
}
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.bg-image {
  background-image: url("https://s3-alpha-sig.figma.com/img/137d/d14c/aa93e0df14a805e8e10c9f1221ba5131?Expires=1652659200&Signature=clqmvJYf9ysOq90pfMx2PM1sO8zTfrjuBKxqmJ4b5hxQpLtoUFWMd45DFMMzOyT5a~32xG6yft7J1KkRjFvd9edId1O2tNfKgmbHaUN6cJcWWEL4Isn-T2Ytnt3Uc-T2SQDkI84C2rsu6bYPDx9OvgV7IXdZXSd5rnaVer2rXnBSMy4nfaY2t8J3U8HF0Y~Jr8fgvhqaBdSbRC9lmyfDGEACGX0YcpRJVULsegQ2ngoeYbK3vtktS-6xQ4j-Ey9fPiKD5YduGW58M27SRwduTCw2eBOycT3OTLGo7jH0Hq0wbpiRihXNDBmXyGtoIwlVQpSo8hXSkTynEO-Qmrk4iA__&Key-Pair-Id=APKAINTVSUGEWH5XD5UA");
  height: 200px;
  background-position: center;
  object-fit: cover;
  backdrop-filter: rgba(0, 0, 0, 0);
}
</style>
