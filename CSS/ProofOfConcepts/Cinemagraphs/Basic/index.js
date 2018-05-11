document.addEventListener("DOMContentLoaded", function(event) {
  scaleVideoContainer();

  initBannerVideoSize(".video-container .poster img");
  initBannerVideoSize(".video-container .filter");
  initBannerVideoSize(".video-container video");

  window.addEventListener("resize", function() {
    scaleVideoContainer();
    scaleBannerVideoSize(".video-container .poster img");
    scaleBannerVideoSize(".video-container .filter");
    scaleBannerVideoSize(".video-container video");
  });
});

function scaleVideoContainer() {
  var height = window.innerHeight + 5;
  var unitHeight = parseInt(height) + "px";
  document.getElementsByClassName(
    "homepage-hero-module"
  )[0].style.height = unitHeight;
}

function initBannerVideoSize(elements) {
  document.querySelectorAll(elements).forEach(element => {
    element.setAttribute("data-height", parseInt(element.height) + "px");
    element.setAttribute("data-width", parseInt(element.width) + "px");
  });

  scaleBannerVideoSize(elements);
}

function scaleBannerVideoSize(elements) {
  var windowWidth = window.innerWidth,
    windowHeight = window.innerHeight + 5,
    videoWidth,
    videoHeight;

  document.querySelectorAll(elements).forEach(element => {
    var videoAspectRatio =
      element.getAttribute("data-height") / element.getAttribute("data-width");

    this.innerWidth = windowWidth;

    if (windowWidth < 1000) {
      videoHeight = windowHeight;
      videoWidth = videoHeight / videoAspectRatio;
      document.querySelectorAll("body")[0].style.marginTop = 0;
      document.querySelectorAll("body")[0].style.marginLeft =
        -(videoWidth - windowWidth) / 2 + "px";

      document.querySelectorAll("body")[0].innerWidth = videoWidth + "px";
      document.querySelectorAll("body")[0].innerHeight = videoHeight + "px";
    }

    document
      .querySelectorAll(".homepage-hero-module .video-container video")[0]
      .classList.add("fadeIn");
    document
      .querySelectorAll(".homepage-hero-module .video-container video")[0]
      .classList.add("animated");
  });
}
