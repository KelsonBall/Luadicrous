function ViewModel()

	local viewModel = {}	

	viewModel.EntryTextIn = BindableProperty("Hello, World!")
	viewModel.EntryTextOut = BindableProperty("Bottom Left")

	viewModel.DisplayTextColor = BindableProperty("#000");

	viewModel.OnTextChanged = function ()
		local text = viewModel.EntryTextIn:Get ()
		if text == "yellow" then
			viewModel.DisplayTextColor:Set("Yellow")
		end
		viewModel.EntryTextOut:Set (text)
	end

	viewModel.Clicks = BindableProperty (0)

	viewModel.OnClick = function()
		local numClicks = viewModel.Clicks:Get ()
		numClicks = numClicks + 1
		viewModel.Clicks:Set(numClicks)
	end


	viewModel.SliderValue = BindableProperty (0)
	viewModel.ProgressValue = BindableProperty (0)

	viewModel.SliderValue.OnSet = function (value)
		viewModel.ProgressValue:Set(value * 10)
	end

	return viewModel

end
