class TourSearch extends React.Component {
    
    render() {
        return (

            <div className="TourSearch">  
                <section className="tour-search">
                    <div className="container">

                        <form asp-action="Index" method="get" className="tour-search-form">

                            <div className="input-wrapper" >
                                <label htmlFor="destination" className="input-label">Search Country/City*</label>

                                <input type="text" name="destination" id="destination" placeholder="Enter Destination"
                                    className="input-field"/>
                            </div>

                            <div className="input-wrapper">
                                <label htmlFor="people" className="input-label">Location*</label>

                                <input type="text" name="location" id="people" placeholder="Place or spot" className="input-field"/>
                            </div>
                            
                            {/*<div className="input-wrapper">*/}
                            {/*    <label htmlFor="checkin" className="input-label">Checkin Date**</label>*/}

                            {/*    <input type="date" name="checkin" id="checkin" required className="input-field"/>*/}
                            {/*</div>*/}

                            {/*<div className="input-wrapper">*/}
                            {/*    <label htmlFor="checkout" className="input-label">Checkout Date*</label>*/}

                            {/*    <input type="date" name="checkout" id="checkout" required className="input-field"/>*/}
                            {/*</div>*/}
                            
                            <button type="submit" className="btn btn-secondary">Inquire now</button>

                        </form>

                    </div>
                </section>


               </div>
        );
    }
}

ReactDOM.render(<TourSearch />, document.getElementById('TourSearch'));