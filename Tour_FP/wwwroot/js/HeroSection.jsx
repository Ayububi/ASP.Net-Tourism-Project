class HeroSection extends React.Component {
    render() {
        return (

            <div className="HeroSection">  

                <section className="hero" id="home">
                    <div className="container">

                        <h2 className="h1 hero-title">Journey to explore pakistan</h2>

                        <p className="hero-text">
                            Discover the hidden treasures of Pakistan, where ancient history meets stunning landscapes in perfect harmony. Immerse yourself in the warm hospitality of its people and be captivated by the magic of a land waiting to be explored
                        </p>



                    </div>
                </section></div>
        );
    }
}

ReactDOM.render(<HeroSection />, document.getElementById('HeroSection'));