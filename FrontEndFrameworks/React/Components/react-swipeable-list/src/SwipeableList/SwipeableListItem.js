import React, { Component } from "react";
import "../styles/SwipeableListItem.css";

class SwipeableListItem extends Component {
    // DOM Refs, necessary to manipulate the different
    // laters and implement swipe functionality
    listElement;
    wrapper;
    background;

    // Drag & Drop
    dragStartX = 0;
    left = 0;
    dragged = false;

    constructor(props) {
        super(props);

        // initialize DOM refs
        this.listElement = null;
        this.wrapper = null;
        this.background = null;

        // bind methods
        this.onMouseMove = this.onMouseMove.bind(this);
        this.onTouchMove = this.onTouchMove.bind(this);
        this.onDragStartMouse = this.onDragStartMouse.bind(this);
        this.onDragStartTouch = this.onDragStartTouch.bind(this);
        this.onDragEndMouse = this.onDragEndMouse.bind(this);
        this.onDragEndTouch = this.onDragEndTouch.bind(this);
        this.onDragEnd = this.onDragEnd.bind(this);
        this.updatePosition = this.updatePosition.bind(this);
        this.onSwiped = this.onSwiped.bind(this);
    }

    /* Drag Start methods */
    onDragStart(clientX) {
        this.dragged = true;
        this.dragStartX = clientX;

        // animation
        this.listElement.className = "list-item";

        // takes care of style adjustments to move
        // foreground to position of the cursor
        requestAnimationFrame(this.updatePosition);
    }

    onDragStartMouse(evt) {
        this.onDragStart(evt.clientX);
        window.addEventListener("mousemove", this.onMouseMove);
    }

    onDragStartTouch(evt) {
        const touch = evt.targetTouches[0];
        this.onDragStart(touch.clientX);
        window.addEventListener("touchmove", this.onTouchMove);
    }

    /* Move methods, subtract the value of dragStartX, which 
    is the offset of the cursor to the top-left of the list item,
    from the position of the cursor */
    onMouseMove(evt) {
        const left = evt.clientX - this.dragStartX;

        // make sure that the result is always lower than zero, 
        // we only want to allow swiping to the left but not to the right
        if (left < 0) {
            this.left = left;
        }
    }

    onTouchMove(evt) {
        const touch = evt.targetTouches[0];
        const left = touch.clientX - this.dragStartX;
        if (left < 0) {
            this.left = left;
        }
    }

    /* Drag End methods */
    onDragEnd() {
        if (this.dragged) {
            this.dragged = false;

            const threshold = this.props.threshold || 0.3;

            if (this.left < this.listElement.offsetWidth * threshold * -1) {
                this.left = -this.listElement.offsetWidth * 2;

                this.wrapper.style.maxHeight = 0;
                this.onSwiped();
            } else {
                this.left = 0;
            }
        }

        // animation
        this.listElement.className = "bouncing-list-item";
        this.listElement.style.transform = `translateX(${this.left}px)`;
    }

    onDragEndMouse(evt) {
        window.removeEventListener("mousemove", this.onMouseMove);
        this.onDragEnd();
    }

    onDragEndTouch(evt) {
        window.removeEventListener("touchmove", this.onTouchMove);
        this.onDragEnd();
    }

    onSwiped() {
        if (this.props.onSwipe) {
            this.props.onSwipe();
        }
    }

    /* Update method */
    updatePosition() {
        if (this.dragged) requestAnimationFrame(this.updatePosition);

        this.listElement.style.transform = `translateX(${this.left}px)`;

        // Fade the opacity
        const opacity = (Math.abs(this.left) / 100).toFixed(2);
        if (opacity < 1 && opacity.toString() !== this.background.style.opacity) {
            this.background.style.opacity = opacity.toString();
        }
        if (opacity >= 1) {
            this.background.style.opacity = "1";
        }
    }

    /* Lifecycle methods */
    componentDidMount() {
        window.addEventListener("mouseup", this.onDragEndMouse);
        window.addEventListener("touchend", this.onDragEndTouch);
    }

    componentWillUnmount() {
        window.removeEventListener("mouseup", this.onDragEndMouse);
        window.removeEventListener("touchend", this.onDragEndTouch);
    }

    // component contains foreground that holds content of the
    // list item, and a background that is revealed when being deleted
    render() {
        return (
            <>
                <div className="wrapper" ref={div => (this.wrapper = div)}>
                    <div ref={div => (this.background = div)} className="background">
                        {this.props.background ? (
                            this.props.background
                        ) : (
                                <span>Delete</span>
                            )}
                    </div>
                    <div
                        className="list-item"
                        ref={div => (this.listElement = div)}
                        onMouseDown={this.onDragStartMouse}
                        onTouchStart={this.onDragStartTouch}
                    >
                        {this.props.children}
                    </div>
                </div>
            </>
        );
    }
}

export default SwipeableListItem;
